using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Savadev.Content.Data.Contract;
using Savadev.Content.Data.Contract.Models;
using Savadev.Content.Data.Entities;
using Savadev.Content.Data.Services.Filters;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;
using SavaDev.Content.Data.Contract.Models;
using X.PagedList;

namespace Savadev.Content.Data.Services
{
    public class DraftService : IDraftService
    {
        private readonly IMapper _mapper;
        private readonly ContentContext _dbContext;
        private readonly ILogger<DraftService> _logger;

        private readonly RestorableContentEntityManager<Draft, DraftModel, DraftFilterModel> entityManager;

        public DraftService(ContentContext dbContext, IMapper mapper, ILogger<DraftService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;

            entityManager = new RestorableContentEntityManager<Draft, DraftModel, DraftFilterModel>(dbContext, mapper, logger);
        }

        public async Task<OperationResult<DraftModel>> Create<T>(DraftModel model, T contentModel) => await entityManager.Create(model, contentModel);


        public async Task<OperationResult> Update<T>(Guid id, T contentModel) => await entityManager.Update(id, contentModel);

        public async Task<OperationResult> SetContentId(Guid id, string contentId)
        {
            var res = await entityManager.Update(id,
                entity => entity.ContentId = contentId
                );
            return res;
        }

        public async Task<OperationResult> Delete(Guid id) => await entityManager.Delete(id);

        public async Task<OperationResult> Restore(Guid id) => await entityManager.Restore(id);

        public async Task<DraftModel> GetOne(Guid id)
        {
            var entity = await _dbContext.Drafts.FirstOrDefaultAsync(m => m.Id == id);

            if (entity == null || entity.IsDeleted) throw new Exception($"Draft {id} not found");

            return _mapper.Map<DraftModel>(entity);
        }

        public async Task<ItemsPage<DraftModel>> GetAll(RegistryQuery<DraftStrictFilterModel> query)
        {
            var result = await GetAll(query, _mapper);

            return result;
        }

        private async Task<ItemsPage<DraftModel>> GetAll(RegistryQuery<DraftStrictFilterModel> query,
            IMapper mapper)
        {
            var list = _dbContext.Set<Draft>().AsQueryable();

            list = list.ApplyStrictFilters(query.Filter);

            list = list.OrderByDescending(m => m.LastUpdated);

            var res = await list.ProjectTo<DraftModel>(mapper.ConfigurationProvider).ToPagedListAsync(query.PageInfo.PageNumber, query.PageInfo.RowsCount);

            var page = new ItemsPage<DraftModel>(res);

            return page;
        }

        public async Task<ItemsPage<DraftModel>> GetAll(RegistryQuery<DraftFilterModel> query)
        {
            var page = await entityManager.GetAll(query, ApplyFilters);
            return page;
        }

        private IQueryable<Draft> ApplyFilters(IQueryable<Draft> list, DraftFilterModel filter)
        {
            return list.ApplyFilters(filter);
        }
    }
}
