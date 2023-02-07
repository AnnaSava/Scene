using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Savadev.Content.Data.Entities;
using SavaDev.Base.Data.Entities.Interfaces;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Models.Interfaces;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Registry.Filter;
using SavaDev.Base.Data.Services;
using SavaDev.Content.Data;
using SavaDev.Content.Data.Entities;
using System.Text.Json;
using X.PagedList;

namespace Savadev.Content.Data.Services
{
    public class BaseContentRestorableEntityService<TEntity, TModel, TFilterModel> 
        where TEntity : BaseContentEntity, IEntity<Guid>, IEntityRestorable
        where TFilterModel : BaseFilter, new()
        where TModel: IFormModel, IAnyModel
    {
        protected readonly IMapper _mapper;
        protected readonly ContentContext _dbContext;
        protected readonly ILogger _logger;

        #region Protected Properties: Managers

        protected CreateManager<TEntity> CreateManager { get; }
        protected UpdateRestorableSelector<Guid, TEntity> UpdateSelector { get; }
        protected UpdateFieldManager<Guid, TEntity> FieldSetterManager { get; }
        protected UpdateFieldManager<Guid, TEntity> RestoreManager { get; }
        protected RestoreSelector<Guid, TEntity> RestoreSelector { get; }
        protected OneRestorableSelector<Guid, TEntity> OneSelector { get; }
        protected AllSelector<Guid, TEntity> AllSelector { get; set; }

        #endregion

        public BaseContentRestorableEntityService(ContentContext dbContext, IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OperationResult> Create<T>(TModel model, T contentModel)
        {

            var result = await CreateManager.Create(model, setValues: async (entity) =>
            {
                entity.Id = Guid.NewGuid();
                entity.Content = JsonSerializer.Serialize(contentModel);
                entity.Created = DateTime.UtcNow;
                return entity;
            });

            return result;
        }

        public async Task<OperationResult> Update<T>(Guid id, T contentModel)
        {
            var result = await FieldSetterManager.SetField(id, entity => entity.Content = JsonSerializer.Serialize(contentModel));
            return result;
        }

        public async Task<OperationResult> Delete(Guid id) => await FieldSetterManager.SetField(id, m=>m.IsDeleted = true);

        public async Task<OperationResult> Restore(Guid id) => await RestoreManager.SetField(id, m => m.IsDeleted = false);

        public async Task<TModel> GetOne(Guid id) => await OneSelector.GetOne<TModel>(id);

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
