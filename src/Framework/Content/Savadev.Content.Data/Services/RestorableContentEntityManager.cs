using AutoMapper;
using AutoMapper.QueryableExtensions;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.DataService.Entities;
using Framework.Base.DataService.Services.Managers;
using Framework.Base.Types.Registry;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Savadev.Content.Data.Entities;
using System.Text.Json;
using X.PagedList;

namespace Savadev.Content.Data.Services
{
    public class RestorableContentEntityManager<TEntity, TModel, TFilterModel>
        where TEntity : BaseContentEntity, IEntity<Guid>, IEntityRestorable
        where TFilterModel : IFilter, new()
    {
        private readonly IMapper _mapper;
        private readonly ContentContext _dbContext;
        private readonly ILogger _logger;

        protected readonly EditableRestorableEntityManager<Guid, TEntity, TModel> entityManager;

        public RestorableContentEntityManager(ContentContext dbContext, IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;

            entityManager = new EditableRestorableEntityManager<Guid, TEntity, TModel>(dbContext, mapper, logger);
        }

        public async Task<OperationResult<TModel>> Create<T>(TModel model, T contentModel)
        {
            var res = await entityManager.Create(model, entity =>
            {
                entity.Id = Guid.NewGuid();
                entity.Content = JsonSerializer.Serialize(contentModel);
                entity.Created = DateTime.Now;
            });

            return res;
        }

        public async Task<OperationResult> Update<T>(Guid id, T contentModel)
        {
            var res = await entityManager.Update(id, 
                entity => entity.Content = JsonSerializer.Serialize(contentModel)
                );
            return res;
        }

        public async Task<OperationResult> Update(Guid id, Action<TEntity> updatingMethod) => await entityManager.Update(id, updatingMethod);

        public async Task<OperationResult> Delete(Guid id) => await entityManager.Delete(id);

        public async Task<OperationResult> Restore(Guid id) => await entityManager.Restore(id);

        public async Task<TModel> GetOne(Guid id) => await entityManager.GetOne<TModel>(id);

        public async Task<PageListModel<TModel>> GetAll(ListQueryModel<TFilterModel> query, Func<IQueryable<TEntity>, TFilterModel, IQueryable<TEntity>> filter)
        {
            var list = _dbContext.Set<TEntity>().AsQueryable().AsNoTracking();

            if (filter != null)
            {
                list = filter(list, query.Filter);
            }

            list = list.OrderByDescending(m => m.Created);

            var res = await list.ProjectTo<TModel>(_mapper.ConfigurationProvider).ToPagedListAsync(query.PageInfo.PageNumber, query.PageInfo.RowsCount);

            var page = new PageListModel<TModel>()
            {
                Items = res,
                Page = res.PageNumber,
                TotalPages = res.PageCount,
                TotalRows = res.TotalItemCount
            };

            return page;
        }
    }
}
