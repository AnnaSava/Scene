using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Services;
using SavaDev.Base.User.Data.Entities;
using SavaDev.Base.User.Data.Manager;
using SavaDev.Base.User.Data.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Services
{
    public abstract class BaseRoleDbService<TEntity, TFormModel>
        where TEntity : BaseRole
        where TFormModel : BaseRoleModel
    {
        #region Protected Fields: Dependencies

        protected readonly IDbContext _dbContext;
        protected readonly RoleManager<TEntity> _roleManager;
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;

        #endregion

        #region Protected Properties: Managers

        protected RoleEntityManager<long, TEntity, TFormModel> EntityManager { get; }

        #endregion

        #region Public Constructors

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

            EntityManager = new RoleEntityManager<long, TEntity, TFormModel>(dbContext, _roleManager, mapper, logger);
        }

        #endregion

        #region Public Methods: Mutation

        public async Task<OperationResult> Create(TFormModel model)
            => await EntityManager.Create(model);
        public async Task<OperationResult> Update(long id, TFormModel model)
           => await EntityManager.Update(id, model);
        public async Task<OperationResult> Delete(long id)
             => await EntityManager.Delete(id);
        public async Task<OperationResult> Restore(long id)
             => await EntityManager.Restore(id);

        #endregion

        #region Public Methods: Query One

        public async Task<TModel> GetOne<TModel>(long id) where TModel : BaseRoleModel
            => await EntityManager.GetOne<TModel>(id);
        public async Task<IEnumerable<TModel>> GetByNames<TModel>(IEnumerable<string> names)
            => await EntityManager.GetByNames<TModel>(names);
        public async Task<bool> CheckNameExists(string roleName)
             => await EntityManager.CheckRoleNameExists(roleName);

        #endregion
    }
}
