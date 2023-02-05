using AutoMapper;
using AutoMapper.QueryableExtensions;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.DataService.Exceptions;
using Framework.Base.DataService.Services;
using Framework.Base.Exceptions;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Models;
using Framework.User.DataService.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.User.DataService.Services
{
    [Obsolete]
    public abstract class BaseRoleDbService<TRoleEntity, TRoleClaimEntity, TRoleModel, TFilterModel>
        where TRoleEntity : BaseRole
        where TRoleClaimEntity: IdentityRoleClaim<long>, new()
        where TRoleModel : BaseRoleModel
        where TFilterModel : ListFilterModel, new()
    {
        protected readonly IRoleContext<TRoleEntity, TRoleClaimEntity> _dbContext;
        private readonly IRoleManagerAdapter<TRoleEntity> _roleManagerAdapter;
        protected readonly IMapper _mapper;

        public BaseRoleDbService(
            IRoleContext<TRoleEntity, TRoleClaimEntity> dbContext,
            IRoleManagerAdapter<TRoleEntity> roleManagerAdapter,
            IMapper mapper,
            string serviceName
            )
        {
            _dbContext = dbContext;

            _roleManagerAdapter = roleManagerAdapter;

            _mapper = mapper ?? throw new ProjectArgumentException(
                GetType(),
                serviceName,
                nameof(mapper),
                null);
        }

        public async Task<TRoleModel> GetOne(long id)
        {
            static void Include(ref IQueryable<TRoleEntity> list, string include)
            {
                //_dbContext.Users.Where(m => m.Id == 1).Include(m => m.UserClaims);
                //if (include == "userClaims")
                //{
                //     list.Include(m => m..RoleClaims);
                //}
            }

            // TODO разобраться с include
            //var role = await _dbContext.GetOne<TRoleEntity, TRoleModel>(id, _mapper, null, null);
            var role = await _dbContext.GetEntityForView<TRoleEntity>(id);
            var claims = await _roleManagerAdapter.GetClaims(role);

            var roleModel = _mapper.Map<TRoleModel>(role);
            roleModel.Permissions = claims.Where(m => m.Type == RoleManagerAdapter<TRoleEntity>.PermissionClaimType).Select(m => m.Value);

            return roleModel;
        }

        public async Task<TRoleModel> Create(TRoleModel model)
        {
            // TODO подумать, оставить ли здесь эту проверку или повесить уникальный индекс на уровне БД?
            if (await CheckRoleNameExists(model.Name))
                throw new EntityAlreadyExistsException(
                    typeof(BaseRoleDbService<TRoleEntity, TRoleClaimEntity, TRoleModel, TFilterModel>),
                    nameof(Create),
                    typeof(TRoleEntity),
                    nameof(BaseRole.Name),
                    model.Name);

            var entity = _mapper.Map<TRoleEntity>(model);

            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                var created = await _roleManagerAdapter.CreateAsync(entity);
                await _roleManagerAdapter.CreatePermissions(created, model.Permissions);

                await transaction.CommitAsync();

                return _mapper.Map<TRoleModel>(created);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<TRoleModel> Update(TRoleModel model)
        {
            var currentEntity = await _dbContext.GetEntityForUpdate<TRoleEntity>(model.Id);

            if (currentEntity.Name != model.Name)
            {
                // TODO подумать, оставить ли здесь эту проверку или повесить уникальный индекс на уровне БД?
                if (await CheckRoleNameExists(model.Name))
                    throw new EntityAlreadyExistsException(
                        typeof(BaseRoleDbService<TRoleEntity, TRoleClaimEntity, TRoleModel, TFilterModel>),
                        nameof(Update),
                        typeof(TRoleEntity),
                        nameof(BaseRole.Name),
                        model.Name);
            }

            _mapper.Map(model, currentEntity);

            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                var updated = await _roleManagerAdapter.UpdateAsync(currentEntity);
                await _roleManagerAdapter.UpdatePermissions(updated, model.Permissions);

                await transaction.CommitAsync();

                return _mapper.Map<TRoleModel>(updated);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<TRoleModel> Delete(long id)
        {
            var entity = await _dbContext.GetEntityForUpdate<TRoleEntity>(id);
            entity.IsDeleted = true;
            var deleted = await _roleManagerAdapter.UpdateAsync(entity);

            return _mapper.Map<TRoleModel>(deleted);
        }

        public async Task<TRoleModel> Restore(long id)
        {
            var entity = await _dbContext.GetEntityForRestore<TRoleEntity>(id);
            entity.IsDeleted = false;
            var restored = await _roleManagerAdapter.UpdateAsync(entity);

            return _mapper.Map<TRoleModel>(restored);
        }

        public async Task<PageListModel<TRoleModel>> GetAll(ListQueryModel<TFilterModel> query)
        {
            return await _dbContext.GetAll<TRoleEntity, TRoleModel, TFilterModel>(query, ApplyFilters, _mapper);
        }

        public async Task<IEnumerable<TRoleModel>> GetByNames(IEnumerable<string> names)
        {
            names = names.Select(m => m.ToUpper());
            return await _dbContext.Set<TRoleEntity>()
                .Where(m => names.Contains(m.NormalizedName))
                .ProjectTo<TRoleModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<bool> CheckRoleNameExists(string roleName)
        {
            if (string.IsNullOrEmpty(roleName)) return false;
            return await _dbContext.Set<TRoleEntity>().AnyAsync(m => m.NormalizedName == roleName.ToUpper());
        }

        protected abstract void ApplyFilters(ref IQueryable<TRoleEntity> list, TFilterModel filter);
    }
}
