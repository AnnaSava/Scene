using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Savadev.Content.Data.Entities;
using SavaDev.Base.Data.Entities.Interfaces;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Registry.Filter;
using SavaDev.Base.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using X.PagedList;

namespace Savadev.Content.Data.Services
{
    public class ContentEntityManager<TEntity, TModel, TFilterModel>
        where TEntity : BaseContentEntity, IEntity<Guid>
        where TFilterModel : BaseFilter, new()
    {
        private readonly IMapper _mapper;
        private readonly ContentContext _dbContext;
        private readonly ILogger _logger;

        protected readonly EditableEntityManager<Guid, TEntity, TModel> entityManager;

        public ContentEntityManager(ContentContext dbContext, IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;

            entityManager = new EditableEntityManager<Guid, TEntity, TModel>(dbContext, mapper, logger);
        }

        public async Task<OperationResult<TModel>> Create<T>(TModel model, T contentModel)
        {
            var res = await entityManager.Create(model, entity =>
            {
                entity.Id = Guid.NewGuid();
                entity.Content = JsonSerializer.Serialize(contentModel);
                entity.Created = DateTime.UtcNow;
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
