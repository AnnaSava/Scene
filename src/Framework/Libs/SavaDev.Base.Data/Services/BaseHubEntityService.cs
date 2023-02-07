using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Entities;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Services
{
    public class BaseHubEntityService<TKey, TEntity, TFormModel> : BaseEntityService<TEntity, TFormModel>
        where TEntity : BaseHubEntity, new()
        where TFormModel: IAnyModel, IFormModel
    {
        #region Protected Properties: Managers

        protected CreateManager<TEntity, TFormModel> CreateManager { get; }
        protected OneSelector<TEntity> OneSelector { get; }
        protected AllSelector<TKey, TEntity> AllSelector { get; set; }

        #endregion

        #region Public Constructors

        public BaseHubEntityService(IDbContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, "", logger)
        {
            CreateManager = new CreateManager<TEntity, TFormModel>(GetInftrastructure);
            OneSelector = new OneSelector<TEntity>(GetInftrastructure);
        }

        #endregion

        #region Public Methods: Mutation

        public async Task<OperationResult> Create(TFormModel model)
        {
            // TODO
            // newEntity.Id = Guid.NewGuid();

            var res =  await CreateManager.Create(model);
            return res;
        }

        #endregion

        #region Public Methods: Query One

        public async Task<TModel> GetOne<TModel>(Guid id)
        {
            var res = await OneSelector.GetOne<TModel>(id);
            return res;
        }

        #endregion
    }
}
