using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Services;
using SavaDev.Base.User.Data.Entities;
using SavaDev.Base.User.Data.Exceptions;
using SavaDev.Base.User.Data.Models;
using SavaDev.Base.Users.Data.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Manager
{
    public class UserEntityManager<TKey, TEntity>
        where TEntity : BaseUser
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
            var user = await _userManager.FindByIdAsync(id.ToString());
            return await _userManager.GetRolesAsync(user);
        }
    }

    public class UserEntityManager<TKey, TEntity, TFormModel> : UserEntityManager<TKey, TEntity>
        where TEntity : BaseUser
        where TFormModel : BaseUserModel
    {
        private readonly IDbContext _dbContext;
        private readonly UserManager<TEntity> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UserEntityManager<TKey, TEntity>> _logger;

        public UserEntityManager(IDbContext dbContext, UserManager<TEntity> userManager, IMapper mapper, ILogger logger)
            : base(dbContext, userManager, mapper, logger)
        {
        }

        #region Public Methods: Mutation

        public async Task<OperationResult<TFormModel>> Create(TFormModel model, string password, Action<TEntity> onCreating = null, Action<TEntity> onCreated = null)
        {
            using var tran = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var newEntity = _mapper.Map<TEntity>(model);
                newEntity.RegDate = newEntity.LastUpdated = DateTime.UtcNow;

                onCreating?.Invoke(newEntity);
                var identityResult = await _userManager.CreateAsync(newEntity, password);
                onCreated?.Invoke(newEntity);

                if (identityResult.Succeeded)
                {
                    var result = new OperationResult<TFormModel>(1, _mapper.Map<TFormModel>(newEntity));

                    await tran.CommitAsync();

                    return result;
                }
                throw new Exception("User not added");
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError($"{nameof(Create)}: {ex.Message} {ex.StackTrace}");
                var result = new OperationResult<TFormModel>(0, model, new OperationExceptionInfo(ex.Message));
                return result;
            }
        }

        public async Task<OperationResult<TFormModel>> Update(TKey id, TFormModel model, Action<TEntity> onUpdating = null, Action<TEntity> onUpdated = null)
        {
            using var tran = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var currentEntity = await FindForUpdate(id);
                _mapper.Map(model, currentEntity);

                onUpdating?.Invoke(currentEntity);
                currentEntity.LastUpdated = DateTime.UtcNow;
                var Identityresult = await _userManager.UpdateAsync(currentEntity);
                onUpdated?.Invoke(currentEntity);

                var result = new OperationResult<TFormModel>(1, _mapper.Map<TFormModel>(currentEntity));

                await tran.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError($"{nameof(Update)}: {ex.Message} {ex.StackTrace}");
                var result = new OperationResult<TFormModel>(0, model, new OperationExceptionInfo(ex.Message));
                return result;
            }
        }

        public async Task<OperationResult> Delete(TKey id, Action<TEntity> onDeleting = null, Action<TEntity> onDeleted = null)
        {
            using var tran = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var entity = await FindForUpdate(id);
                if (entity.UserName == "admin") throw new InvalidOperationException("Removing of user admin is forbidden!");
                entity.IsDeleted = true;

                onDeleting?.Invoke(entity);
                var dbResult = await _userManager.UpdateAsync(entity);
                onDeleted?.Invoke(entity);

                // TODO
                var result = new OperationResult(1, id);

                await tran.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError($"{nameof(Delete)}: {ex.Message} {ex.StackTrace}");
                var result = new OperationResult(0, id, new OperationExceptionInfo(ex.Message));
                return result;
            }
        }

        public async Task<OperationResult> Restore(TKey id, Action<TEntity> onRestoring = null, Action<TEntity> onRestored = null)
        {
            using var tran = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var entity = await FindForUpdate(id);
                entity.IsDeleted = false;

                onRestoring?.Invoke(entity);
                var dbResult = await _userManager.UpdateAsync(entity);
                onRestored?.Invoke(entity);

                // TODO
                var result = new OperationResult(1, id);

                await tran.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError($"{nameof(Delete)}: {ex.Message} {ex.StackTrace}");
                var result = new OperationResult(0, id, new OperationExceptionInfo(ex.Message));
                return result;
            }
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

            return user;
        }

        private async Task<TEntity> FindForUpdate(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || user.IsDeleted) throw new UserNotFoundException();

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
