using AutoMapper;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Managers.Crud;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Registry.Filter;
using SavaDev.Base.Data.Services;

namespace SavaDev.Base.Data.Managers
{
    public class AllSelector<TKey, TEntity>
        where TEntity: class
    {
        protected readonly IDbContext _dbContext;
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;

        private readonly ServiceInftrastructure _infra;

        public AllSelector(IDbContext dbContext, IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public AllSelector(ServiceInftrastructure infrastructure)
        {
            _infra = infrastructure;
            _dbContext = _infra.DbContext;
            _mapper = _infra.Mapper;
            _logger = _infra.Logger;
        }


        public async Task<RegistryPage<TItemModel>> GetRegistryPage<TFilterModel, TItemModel>(RegistryQuery<TFilterModel> query)
            where TFilterModel : BaseFilter
        {
            var selector = new EntitySelector<TKey, TEntity, TItemModel, TFilterModel>(_dbContext, _mapper, _logger);

            var page = await selector.Query(query).ToRegistryPage();
            return page;
        }

        public async Task<ItemsPage<TItemModel>> GetItemsPage<TFilterModel, TItemModel>(
        RegistryQuery<TFilterModel> query,
            Func<IQueryable<TEntity>, RegistryQuery<TFilterModel>, IQueryable<TEntity>> applyFilterExpression)
            where TFilterModel : BaseFilter
        {
            var selector = new EntitySelector<TKey, TEntity, TItemModel, TFilterModel>(_dbContext, _mapper, _logger);

            // TODO query
            throw new NotImplementedException();
            var page = await selector.Query(new RegistryQuery()).ToItemsPage();
            return page;
        }

        public async Task<IEnumerable<TItemModel>> GetAllByIds<TItemModel>(ByIdsFilter<TKey> filter)
        {
            var selector = new EntitySelector<TKey, TEntity, TItemModel, ByIdsFilter<TKey>>(_dbContext, _mapper, _logger);

            var list = await selector.ByIds(filter).ToEnumerable();
            return list;
        }

        public async Task<IEnumerable<TItemModel>> GetAllByRelated<TItemModel>(ByRelatedFilter<long> filter)
        {
            var selector = new EntitySelector<TKey, TEntity, TItemModel, ByRelatedFilter<long>>(_dbContext, _mapper, _logger);

            var list = await selector.ByRelated(filter).ToEnumerable();
            return list;
        }
    }
}
