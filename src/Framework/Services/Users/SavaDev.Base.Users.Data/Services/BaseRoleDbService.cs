using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Exceptions;
using SavaDev.Base.Data.Services;
using SavaDev.Base.User.Data.Entities;
using SavaDev.Base.User.Data.Manager;
using SavaDev.Base.User.Data.Models.Interfaces;
using SavaDev.Base.Users.Data.Manager;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Services
{
    public abstract class BaseRoleDbService<TEntity, TFormModel>
        where TEntity : BaseRole
        where TFormModel : BaseRoleModel
    {
        protected readonly IDbContext _dbContext;
        private readonly RoleManager<TEntity> _roleManager;
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;

        protected readonly RoleEntityManager<long, TEntity, TFormModel> entityManager;

        public BaseRoleDbService(
            IDbContext dbContext,
            RoleManager<TEntity> roleManager,
            IMapper mapper,
            ILogger logger)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _mapper = mapper;
            _logger= logger;

            entityManager = new RoleEntityManager<long, TEntity, TFormModel>(dbContext, _roleManager, mapper, logger);
        }

        public async Task<OperationResult> Create(TFormModel model)
            => await entityManager.Create(model);
        public async Task<OperationResult> Update(long id, TFormModel model)
           => await entityManager.Update(id, model);
        public async Task<OperationResult> Delete(long id)
             => await entityManager.Delete(id);
        public async Task<OperationResult> Restore(long id)
             => await entityManager.Restore(id);

        public async Task<TModel> GetOne<TModel>(long id) where TModel : BaseRoleModel
            => await entityManager.GetOne<TModel>(id);
        public async Task<IEnumerable<TModel>> GetByNames<TModel>(IEnumerable<string> names)
            => await entityManager.GetByNames<TModel>(names);
        public async Task<bool> CheckNameExists(string roleName)
             => await entityManager.CheckRoleNameExists(roleName);
    }
}
