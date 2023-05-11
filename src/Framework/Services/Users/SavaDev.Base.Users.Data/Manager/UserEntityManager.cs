using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Managers.Crud;
using SavaDev.Base.Data.Services;
using SavaDev.Base.User.Data.Entities;
using SavaDev.Base.User.Data.Exceptions;
using SavaDev.Base.User.Data.Models;
using SavaDev.Base.Users.Data.Manager;
using SavaDev.Base.Users.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Manager
{
    public class UserEntityManager<TKey, TEntity>
        where TEntity : BaseUser, new()
    {
        protected readonly IDbContext _dbContext;
        protected readonly UserManager<TEntity> _userManager;
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;
        public UserEntityManager(IDbContext dbContext, UserManager<TEntity> userManager, IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TModel> GetOneByLoginOrEmail<TModel>(string loginOrEmail)
            where TModel : BaseUserModel
        {
            var user = await _userManager.GetOneByLoginOrEmail(loginOrEmail);
            return _mapper.Map<TModel>(user);
        }

        public async Task<IList<string>> GetRoleNames(long id)
        {
            var user = await Find(id.ToString());
            return await _userManager.GetRolesAsync(user);
        }

        protected async Task<TEntity> Find(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || user.IsDeleted) throw new UserNotFoundException();

            return user;
        }
    }

    public class UserEntityManager<TKey, TEntity, TFormModel> : UserEntityManager<TKey, TEntity>
        where TEntity : BaseUser, new()
        where TFormModel : BaseUserModel
    {
        public UserEntityManager(IDbContext dbContext, UserManager<TEntity> userManager, IMapper mapper, ILogger logger)
            : base(dbContext, userManager, mapper, logger)
        {
        }

        #region Public Methods: Mutation

        public async Task<OperationResult> Create(IUserFormModel model, string password)
        {
            var creator = new EntityCreator<TEntity>(_dbContext, _logger)
                .SetValues(async (model) =>
                {
                    var entity = _mapper.Map<TEntity>(model);
                    entity.PasswordHash = password;  // TODO продумать, как красивее прокидывать пароль
                    entity.RegDate = entity.LastUpdated = DateTime.UtcNow;
                    return entity;
                })
                .Create(DoCreate)
                .SuccessResult(entity => new OperationResult(_mapper.Map<TFormModel>(entity)));

            return await creator.Create((TFormModel)model);
        }

        private async Task<OperationResult> DoCreate(TEntity entity)
        {
            var identityResult = await _userManager.CreateAsync(entity, entity.PasswordHash); // TODO продумать, как красивее прокидывать пароль
            if (!identityResult.Succeeded)
                throw new Exception("User was not created");
            return new OperationResult(1);
        }

        public async Task<OperationResult> Update(TKey id, TFormModel model, Action<TEntity> onUpdating = null, Action<TEntity> onUpdated = null)
        {
            var updater = new EntityUpdater<TKey, TEntity>(_dbContext, _logger)
                .ValidateModel(async (model) => { })
                .GetEntity(async (id) => await FindForUpdate(id))
                .SetValues(async (entity, model) => _mapper.Map(model, entity))
                .Update(DoUpdate)
                .SuccessResult(entity => new OperationResult(_mapper.Map<TFormModel>(entity)));

            return await updater.DoUpdate<TFormModel>(id, model);
        }

        private async Task<OperationResult> DoUpdate(TEntity entity)
        {
            var identityResult = await _userManager.UpdateAsync(entity);
            if (!identityResult.Succeeded)
                throw new Exception("User was not update");
            return new OperationResult(1);
        }

        public async Task<OperationResult> Delete(TKey id)
        {
            var updater = new EntityUpdater<TKey, TEntity>(_dbContext, _logger)
                .GetEntity(async (id) =>
                { 
                    var entity = await FindForUpdate(id);
                    if (entity.UserName == "admin") throw new InvalidOperationException("Removing of user admin is forbidden!");
                    return entity;
                })
                .SetValues(async (entity) => { entity.IsDeleted = true; })
                .Update(DoUpdate)
                .SuccessResult(entity => new OperationResult(_mapper.Map<TFormModel>(entity)));

            return await updater.DoUpdate(id);
        }

        public async Task<OperationResult> Restore(TKey id)
        {
            var updater = new EntityUpdater<TKey, TEntity>(_dbContext, _logger)
                .GetEntity(async (id) =>
                {
                    var entity = await FindForRestore(id);                    
                    return entity;
                })
                .SetValues(async (entity) => { entity.IsDeleted = false; })
                .Update(DoUpdate)
                .SuccessResult(entity => new OperationResult(_mapper.Map<TFormModel>(entity)));

            return await updater.DoUpdate(id);
        }

        public async Task<OperationResult<TUserModelOut>> Lock<TUserModelOut>(UserLockoutModel lockoutModel)
        {
            var entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(m => m.Id == lockoutModel.Id);
            entity.LockoutEnabled = true;
            entity.LockoutEnd = lockoutModel.LockoutEnd;

            var lockout = new Lockout()
            {
                LockDate = DateTime.UtcNow,
                LockedByUserId = 1, //TODO прокидывать юзера, кто заблокировал
                LockoutEnd = lockoutModel.LockoutEnd,
                Reason = lockoutModel.Reason,
                UserId = lockoutModel.Id
            };
            _dbContext.Set<Lockout>().Add(lockout);

            await _dbContext.SaveChangesAsync();

            return new OperationResult<TUserModelOut>(1, _mapper.Map<TUserModelOut>(entity));
        }

        public async Task<OperationResult<TUserModelOut>> Unlock<TUserModelOut>(long id)
        {
            var entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(m => m.Id == id);
            entity.LockoutEnabled = false;
            entity.LockoutEnd = null;
            await _dbContext.SaveChangesAsync();

            return new OperationResult<TUserModelOut>(1, _mapper.Map<TUserModelOut>(entity));
        }

        #endregion

        public async Task<TModel> GetOneByLogin<TModel>(string login)
        {
            var entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(m => m.UserName == login && m.IsDeleted == false);
            return _mapper.Map<TModel>(entity);
        }

        public async Task<TModel> GetOneByEmail<TModel>(string email)
        {
            var entity = await GetOneByEmail(email);
            return _mapper.Map<TModel>(entity);
        }

        public async Task<bool> CheckEmailExists(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            return await _dbContext.Set<TEntity>().AnyAsync(m => m.NormalizedEmail == email.ToUpper());
        }

        public async Task<bool> CheckLoginExists(string login)
        {
            if (string.IsNullOrEmpty(login)) return false;
            return await _dbContext.Set<TEntity>().AnyAsync(m => m.NormalizedUserName == login.ToUpper());
        }

        public async Task<OperationResult> UpdateRoles(UserRolesModel model)
        {
            var user = await FindForUpdate(model.UserId.ToString());
            var userRoles = await _userManager.GetRolesAsync(user);

            var rolesToAdd = model.RoleNames.Except(userRoles);

            if (rolesToAdd.Any())
            {
                // TODO
                var result = await _userManager.AddToRolesAsync(user, userRoles);
            }

            var rolesToRemove = userRoles.Except(model.RoleNames);
            if (rolesToRemove.Any())
            {
                var result = await _userManager.RemoveFromRolesAsync(user, userRoles);
            }
            return new OperationResult(1);
        }

        public async Task<OperationResult> AddRoles(UserRolesModel model)
        {
            var user = await FindForUpdate(model.UserId.ToString());
            await _userManager.AddToRolesAsync(user, model.RoleNames);

            // TODO
            return new OperationResult(1);
        }

        public async Task<OperationResult> RemoveRoles(UserRolesModel model)
        {
            var user = await FindForUpdate(model.UserId.ToString());
            await _userManager.RemoveFromRolesAsync(user, model.RoleNames);

            // TODO
            return new OperationResult(1);
        }

        public async Task<bool> IsLocked(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            return user.LockoutEnabled;
        }

        private async Task<TEntity> FindForUpdate(TKey userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null || user.IsDeleted) throw new UserNotFoundException();

            user.LastUpdated = DateTime.UtcNow;

            return user;
        }

        private async Task<TEntity> FindForRestore(TKey userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) throw new UserNotFoundException();

            user.LastUpdated = DateTime.UtcNow;

            return user;
        }

        private async Task<TEntity> FindForUpdate(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || user.IsDeleted) throw new UserNotFoundException();

            user.LastUpdated = DateTime.UtcNow;

            return user;
        }

        public async Task<TEntity> GetOneByEmail(string email)
        {
            var entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(m => m.Email == email && m.IsDeleted == false);
            return entity;
        }

        public async Task<IEnumerable<TModel>> GetAllByIds<TModel>(IEnumerable<string> ids)
        {
            var longIds = ids.Select(m => long.Parse(m));

            var list = await _dbContext.Set<TEntity>()
                .Where(m => longIds.Contains(m.Id))
                .ProjectTo<TModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return list;
        }
    }
}
