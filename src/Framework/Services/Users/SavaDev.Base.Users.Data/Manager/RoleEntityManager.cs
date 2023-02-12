using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Enums;
using SavaDev.Base.Data.Exceptions;
using SavaDev.Base.Data.Managers.Crud;
using SavaDev.Base.Data.Services;
using SavaDev.Base.User.Data.Entities;
using SavaDev.Base.User.Data.Models.Interfaces;
using SavaDev.Base.Users.Data.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Manager
{
    public class RoleEntityManager<TKey, TEntity>
        where TEntity : BaseRole, new()
    {
        protected readonly IDbContext _dbContext;
        protected readonly RoleManager<TEntity> _roleManager;
        protected readonly ILogger _logger;

        public RoleEntityManager(IDbContext dbContext, RoleManager<TEntity> roleManager, ILogger logger)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<TEntity> GetOneByName(string name)
        {
            return await _roleManager.FindByNameAsync(name);
        }

        public async Task<IEnumerable<Claim>> GetClaims(TEntity role)
        {
            return await _roleManager.GetClaimsAsync(role);
        }
    }

    public class RoleEntityManager<TKey, TEntity, TFormModel> : RoleEntityManager<TKey, TEntity>
        where TEntity : BaseRole, new()
        where TFormModel : BaseRoleModel
    {
        const string PermissionClaimType = "permission";
        private readonly IMapper _mapper;

        public RoleEntityManager(IDbContext dbContext, RoleManager<TEntity> roleManager, IMapper mapper, ILogger logger)
            : base (dbContext, roleManager, logger)
        {
            _mapper = mapper;
        }

        #region Public Methods: Mutation

        public async Task<OperationResult> Create(TFormModel model)
        {
            var creator = new EntityCreator<TEntity>(_dbContext, _logger)
                .ValidateModel(async (model) =>
                {
                    if (await CheckRoleNameExists((model as TFormModel).Name))
                        throw new EntityAlreadyExistsException();
                })
                .SetValues(async (model) => _mapper.Map<TEntity>(model))
                .Create(DoCreate)
                .AfterCreate(async (entity, model) => await CreatePermissions(entity, (model as TFormModel).Permissions))
                .SuccessResult(entity => new OperationResult(1, _mapper.Map<TFormModel>(entity)))
                .ErrorResult((id, errMessage) => new OperationResult(DbOperationRows.OnFailure, id, new OperationExceptionInfo(errMessage)));

            return await creator.Create(model);
        }

        public async Task<OperationResult> Update(TKey id, TFormModel model)
        {
            var updater = new EntityUpdater<TKey, TEntity>(_dbContext, _logger)
                .ValidateModel(async (model) =>
                {
                    if (await CheckRoleNameExists((model as TFormModel).Name))
                        throw new EntityAlreadyExistsException();
                })
                .GetEntity(async (id) => await GetEntityForUpdate(id))
                .SetValues(async (entity, model) => _mapper.Map(model, entity))
                .Update(DoUpdate)
                .AfterUpdate(async (entity, model) => await UpdatePermissions(entity, (model as TFormModel).Permissions))
                .SuccessResult(entity => new OperationResult(1, _mapper.Map<TFormModel>(entity)))
                .ErrorResult((id, errMessage) => new OperationResult(DbOperationRows.OnFailure, id, new OperationExceptionInfo(errMessage)));

            return await updater.DoUpdate<TFormModel>(id, model);
        }

        public async Task<OperationResult> Delete(TKey id) => await SetField(id, entity => entity.IsDeleted = true);

        public async Task<OperationResult> Restore(TKey id)
        {
            var updater = new EntityUpdater<TKey, TEntity>(_dbContext, _logger)
                .GetEntity(async (id) => await GetEntityForUpdate(id, true))
                .SetValues(async (entity) => entity.IsDeleted = false)
                .Update(DoUpdate)
                .SuccessResult(entity => new OperationResult(1, _mapper.Map<TFormModel>(entity)))
                .ErrorResult((id, errMessage) => new OperationResult(DbOperationRows.OnFailure, id, new OperationExceptionInfo(errMessage)));

            return await updater.DoUpdate(id, true);
        }

        #endregion

        public async Task<TModel> GetOne<TModel>(TKey id)
            where TModel : BaseRoleModel
        {
            // TODO разобраться с include
            //var role = await _dbContext.GetOne<TRoleEntity, TRoleModel>(id, _mapper, null, null);
            var role = await GetEntityForView(id);
            var claims = await GetClaims(role);

            var roleModel = _mapper.Map<TModel>(role);
            roleModel.Permissions = claims.Where(m => m.Type == PermissionClaimType).Select(m => m.Value);

            return roleModel;
        }

        public async Task<IEnumerable<TModel>> GetByNames<TModel>(IEnumerable<string> names)
        {
            names = names.Select(m => m.ToUpper());
            return await _dbContext.Set<TEntity>()
                .Where(m => names.Contains(m.NormalizedName))
                .ProjectTo<TModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<bool> CheckRoleNameExists(string roleName)
        {
            if (string.IsNullOrEmpty(roleName)) return false;
            return await _dbContext.Set<TEntity>().AnyAsync(m => m.NormalizedName == roleName.ToUpper());
        }

        public async Task<TEntity> GetEntityForUpdate(TKey id, bool restore = false)
        {
            var entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(m => m.Id.Equals(id) && m.IsDeleted == restore);

            if (entity == null)
                throw new EntityNotFoundException();

            entity.LastUpdated = DateTime.UtcNow;

            return entity;
        }

        public async Task<TEntity> GetEntityForView(TKey id)
        {
            var entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(m => m.Id.Equals(id) && m.IsDeleted == false);
            return entity;
        }

        #region Private Methods

        private async Task<OperationResult> DoCreate(TEntity entity)
        {
            var result = await _roleManager.CreateAsync(entity);
            return new OperationResult(result.Succeeded ? 1 : (int)DbOperationRows.OnFailure); // не уверена, что так красиво
        }

        private async Task<OperationResult> CreatePermissions(TEntity role, IEnumerable<string> permissions)
        {
            if (permissions == null || !permissions.Any()) return new OperationResult(0);

            permissions = permissions.Distinct();

            int rows = 0;
            foreach (var permission in permissions)
            {
                var claim = new Claim(PermissionClaimType, permission);
                var result = await _roleManager.AddClaimAsync(role, claim);
                if (!result.Succeeded)
                {
                    return new OperationResult(rows, permission, new OperationExceptionInfo(result.Errors.GetString()));
                }
                rows++;
            }

            return new OperationResult(rows);
        }

        private async Task<OperationResult> DoUpdate(TEntity entity)
        {
            var result = await _roleManager.UpdateAsync(entity);
            return new OperationResult(result.Succeeded ? 1 : (int)DbOperationRows.OnFailure);
        }

        private async Task<OperationResult> SetField(TKey id, Func<TEntity, bool> fieldFunc)
        {
            var updater = new EntityUpdater<TKey, TEntity>(_dbContext, _logger)
                .GetEntity(async (id) => await GetEntityForUpdate(id))
                .SetValues(async (entity) => fieldFunc(entity))
                .Update(DoUpdate)
                .SuccessResult(entity => new OperationResult(1, _mapper.Map<TFormModel>(entity)))
                .ErrorResult((id, errMessage) => new OperationResult(DbOperationRows.OnFailure, id, new OperationExceptionInfo(errMessage)));

            return await updater.DoUpdate(id);
        }

        private async Task<OperationResult> UpdatePermissions(TEntity role, IEnumerable<string> permissions)
        {
            if (permissions == null) return new OperationResult(0);

            permissions = permissions.Distinct();

            var curClaims = await _roleManager.GetClaimsAsync(role);
            var curPermissions = curClaims.Where(m => m.Type == PermissionClaimType);

            var deletedPermissions = curPermissions.Where(m => !permissions.Contains(m.Value));

            int rows = 0;
            foreach (var deletedPermission in deletedPermissions)
            {
                var result = await _roleManager.RemoveClaimAsync(role, deletedPermission);
                if (!result.Succeeded)
                {
                    return new OperationResult(rows, deletedPermission, new OperationExceptionInfo(result.Errors.GetString()));
                }
                rows++;
            }

            foreach (var permission in permissions)
            {
                if (curPermissions.Any(m => m.Type == PermissionClaimType && m.Value == permission))
                    continue;

                var result = await _roleManager.AddClaimAsync(role, new Claim(PermissionClaimType, permission));
                if (!result.Succeeded)
                {
                    return new OperationResult(rows, permission, new OperationExceptionInfo(result.Errors.GetString()));
                }
                rows++;
            }
            return new OperationResult(rows);
        }

        // TODO возможно, пригодится
        private void HandleResult(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.First().Description);
            }
        }

        #endregion
    }
}
