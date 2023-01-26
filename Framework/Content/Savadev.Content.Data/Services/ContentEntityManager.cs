using AutoMapper;
using AutoMapper.QueryableExtensions;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.DataService.Entities;
using Framework.Base.DataService.Services.Managers;
using Framework.Base.Types.View;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Savadev.Content.Data.Entities;
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
        where TFilterModel : IFilter, new()
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
                entity.Id = new Guid();
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
