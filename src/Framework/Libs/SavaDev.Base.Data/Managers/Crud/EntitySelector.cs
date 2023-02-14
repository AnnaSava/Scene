using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Entities;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Registry.Filter;
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

        //Func<IQueryable<TEntity>, RegistryQuery<TFilter>, IQueryable<TEntity>>? FilterExpression { get; set; }

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
            RegistryQuery = query;
            return this;
        }

        public EntitySelector<TKey, TEntity, TItemModel, TFilter> Filter(BaseFilter filter)
        {
            if (RegistryQuery == null) RegistryQuery = new RegistryQuery();
            RegistryQuery.Filter0 = filter;
            return this;
        }

        //public EntitySelector<TKey, TEntity, TItemModel, TFilter> Filter(Func<IQueryable<TEntity>, RegistryQuery<TFilter>, IQueryable<TEntity>> filter)
        //{
        //    FilterExpression = filter;
        //    return this;
        //}

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

            //if(RegistryQuery?.Filter0 != null)
            {
                list = list.ApplyFilters(RegistryQuery.Filter0);
            }
            //if(FilterExpression!= null)
            //{
            //    list = FilterExpression(list);
            //}
            if (list is IQueryable<BaseRestorableEntity<TKey>>)
            {
                list = ApplyFiltersRestorable(list as IQueryable<BaseRestorableEntity<TKey>>);
            }
            if (RegistryQuery?.Sort != null)
            {
                list = list.ApplySort(RegistryQuery.Sort);
            }
            return list;
        }

        #endregion

        #region Private Methods

        private IQueryable<TEntity> ApplyFiltersRestorable(IQueryable<BaseRestorableEntity<TKey>> list)
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
            return (IQueryable<TEntity>)list;
        }

        private IQueryable<TEntity> GetList()
        {
            return _dbContext.Set<TEntity>()
                .AsQueryable()
                .AsNoTracking();
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
