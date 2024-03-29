﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Entities;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Registry.Filter;
using System.Linq.Expressions;
using X.PagedList;

namespace SavaDev.Base.Data.Managers.Crud
{
    public class EntitySelector<TKey, TEntity, TItemModel, TFilter>
        where TEntity : class
    {
        #region Private Fields: Dependencies

        protected readonly IDbContext _dbContext;
        protected readonly ILogger _logger;
        protected readonly IMapper _mapper;

        #endregion

        #region Private Properties

        private RegistryQuery RegistryQuery { get; set; } = new RegistryQuery(1, 20);

        private ByIdsFilter<TKey>? ByIdsFilter { get; set; }
        private ByRelatedFilter<long>? ByRelatedFilter { get; set; }

        private Expression<Func<TEntity, bool>>? FilterExpression0 { get; set; }

        Func<IQueryable<TEntity>, RegistryQuery, IQueryable<TEntity>>? FilterExpression { get; set; }

        #endregion

        #region Public Constructors

        public EntitySelector(IDbContext dbContext, IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods: Set

        public EntitySelector<TKey, TEntity, TItemModel, TFilter> Query(RegistryQuery query)
        {
            if (query == null) RegistryQuery = new RegistryQuery();
            else RegistryQuery = query;
            return this;
        }

        public EntitySelector<TKey, TEntity, TItemModel, TFilter> Filter(BaseFilter filter)
        {
            if (RegistryQuery == null) RegistryQuery = new RegistryQuery();
            RegistryQuery.Filter0 = filter;
            return this;
        }

        public EntitySelector<TKey, TEntity, TItemModel, TFilter> Filter(Expression<Func<TEntity, bool>> filterExpression)
        {
            FilterExpression0 = filterExpression;
            return this;
        }

        public EntitySelector<TKey, TEntity, TItemModel, TFilter> Filter(Func<IQueryable<TEntity>, RegistryQuery, IQueryable<TEntity>> filter)
        {
            FilterExpression = filter;
            return this;
        }

        public EntitySelector<TKey, TEntity, TItemModel, TFilter> ByIds(ByIdsFilter<TKey> filter)
        {
            ByIdsFilter = filter;
            return this;
        }

        public EntitySelector<TKey, TEntity, TItemModel, TFilter> ByRelated(ByRelatedFilter<long> filter)
        {
            ByRelatedFilter = filter;
            return this;
        }

        public EntitySelector<TKey, TEntity, TItemModel, TFilter> Sort(RegistrySort sort)
        {
            if (RegistryQuery == null) RegistryQuery = new RegistryQuery();
            RegistryQuery.SetNewSort(sort);
            return this;
        }

        public EntitySelector<TKey, TEntity, TItemModel, TFilter> PageInfo(RegistryPageInfo pageInfo)
        {
            if (RegistryQuery == null) RegistryQuery = new RegistryQuery();
            RegistryQuery.PageInfo = pageInfo;
            return this;
        }

        public EntitySelector<TKey, TEntity, TItemModel, TFilter> PageInfo(int pageNumber, int rowsCount)
        {
            if (RegistryQuery == null) RegistryQuery = new RegistryQuery();
            RegistryQuery.PageInfo.PageNumber = pageNumber;
            RegistryQuery.PageInfo.RowsCount= rowsCount;
            return this;
        }

        #endregion

        #region Public Methods: Fetch

        public async Task<RegistryPage<TItemModel>> ToRegistryPage()
        {
            var list = DoGet();

            var res = await ProjectToPage(list, RegistryQuery.PageInfo);
            var page = new RegistryPage<TItemModel>(res);
            return page;
        }

        public async Task<ItemsPage<TItemModel>> ToItemsPage()
        {
            var list = DoGet();

            var res = await ProjectToPage(list, RegistryQuery.PageInfo);
            var page = new ItemsPage<TItemModel>(res);
            return page;
        }

        public async Task<IEnumerable<TItemModel>> ToEnumerable()
        {
            var list = DoGet();

            var res = await ProjectToList(list);
            return res;
        }

        public async Task<IPagedList<TItemModel>> ToPagedList()
        {
            var list = DoGet();

            var res = await ProjectToPage(list, RegistryQuery.PageInfo);
            return res;
        }

        #endregion

        #region Public Methods: Act

        public IQueryable<TEntity> DoGet()
        {
            var list = GetList();

            if (FilterExpression != null)
            {
                list = FilterExpression(list, RegistryQuery);
            }
            if (FilterExpression0 != null)
            {
                list = list.Where(FilterExpression0);
            }
            if (RegistryQuery?.Filter0 != null)
            {
                try
                {
                    list = list.ApplyFilters(RegistryQuery.Filter0);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            //if (list is IQueryable<BaseRestorableEntity<TKey>>)
            //{
            //    var list0 = list as IQueryable<BaseRestorableEntity<TKey>>;
            //    list0 = ApplyFiltersRestorable(list0);
            //    //list = list0;
            //}
            if (RegistryQuery?.Sort != null)
            {
                list = list.ApplySort(RegistryQuery.Sort);
            }
            return list;
        }

        #endregion

        #region Private Methods

        private IQueryable<BaseRestorableEntity<TKey>> ApplyFiltersRestorable(IQueryable<BaseRestorableEntity<TKey>> list)
        {
            if (ByIdsFilter != null)
            {
                list = list.Where(m => ByIdsFilter.Ids.Contains(m.Id));

                if (!ByIdsFilter.WithDeleted)
                {
                    list = list.Where(m => !m.IsDeleted);
                }
            }

            if (ByRelatedFilter != null)
            {
                list = list.ApplyByRelatedFilter(ByRelatedFilter);

                if (!ByRelatedFilter.WithDeleted)
                {
                    list = list.Where(m => !m.IsDeleted);
                }
            }
            return list;
        }

        private IQueryable<TEntity> GetList()
        {
            try
            {
                return _dbContext.Set<TEntity>()
                    .AsQueryable()
                    .AsNoTracking();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<List<TItemModel>> ProjectToList(IQueryable<TEntity> list)
        {
            var res = await list
                .ProjectTo<TItemModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return res;
        }

        private async Task<IPagedList<TItemModel>> ProjectToPage(IQueryable<TEntity> list, RegistryPageInfo pageInfo)
        {
          var res =  await list
                .ProjectTo<TItemModel>(_mapper.ConfigurationProvider)
                .ToPagedListAsync(pageInfo.PageNumber, pageInfo.RowsCount);
            return res;
        }

        #endregion
    }
}
