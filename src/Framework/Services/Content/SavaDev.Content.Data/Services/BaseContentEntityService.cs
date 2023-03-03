using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Content.Data.Entities;
using SavaDev.Base.Data.Entities.Interfaces;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Models.Interfaces;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Registry.Filter;
using SavaDev.Base.Data.Services;
using SavaDev.Content.Data;
using SavaDev.Content.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using X.PagedList;

namespace SavaDev.Content.Data.Services
{
    public class BaseContentEntityService<TEntity, TModel, TFilterModel>
        where TEntity : BaseContentEntity, IEntity<Guid>, new()
        where TFilterModel : BaseFilter, new()
        where TModel: IFormModel
    {
        #region Private Fields: Dependencies

        protected readonly IMapper _mapper;
        protected readonly ContentContext _dbContext;
        protected readonly ILogger _logger;

        #endregion

        #region Protected Properties: Managers

        private CreateManager<TEntity> CreateManager { get; }
        private UpdateManager<Guid, TEntity, TModel> UpdateManager { get; }
        protected AllSelector<Guid, TEntity> AllSelector { get; set; }

        #endregion

        #region Public Constructors

        public BaseContentEntityService(ContentContext dbContext, IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;

            CreateManager = new CreateManager<TEntity> (dbContext, mapper, logger);
            UpdateManager = new UpdateManager<Guid, TEntity, TModel>(dbContext, mapper, logger);
        }

        #endregion

        #region Public Methods: Mutation

        public async Task<OperationResult> Create<T>(TModel model, T contentModel)
        {
            var res = await CreateManager.Create(model, setValues: async (entity) =>
            {
                entity.Id = Guid.NewGuid();
                entity.Content = JsonSerializer.Serialize(contentModel);
                entity.Created = DateTime.UtcNow;
                return entity;
            });
            return res;
        }

        public async Task<OperationResult> Update<T>(Guid id, T contentModel)
        {
            var res = await UpdateManager.SetField(id, e => e.Content = JsonSerializer.Serialize(contentModel));
            return res;
        }

        #endregion

        public async Task<ItemsPage<TModel>> GetAll(RegistryQuery<TFilterModel> query, Func<IQueryable<TEntity>, TFilterModel, IQueryable<TEntity>> filter)
        {
            var list = _dbContext.Set<TEntity>().AsQueryable().AsNoTracking();

            if (filter != null)
            {
                list = filter(list, query.Filter);
            }

            list = list.OrderByDescending(m => m.Created);

            var res = await list.ProjectTo<TModel>(_mapper.ConfigurationProvider).ToPagedListAsync(query.PageInfo.PageNumber, query.PageInfo.RowsCount);

            var page = new ItemsPage<TModel>(res);

            return page;
        }
    }
}
