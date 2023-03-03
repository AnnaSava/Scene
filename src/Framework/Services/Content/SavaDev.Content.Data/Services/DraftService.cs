using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Logging;
using SavaDev.Content.Data.Contract;
using SavaDev.Content.Data.Contract.Models;
using SavaDev.Content.Data.Entities;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;
using SavaDev.Content.Data;
using SavaDev.Content.Data.Contract.Models;
using X.PagedList;

namespace SavaDev.Content.Data.Services
{
    public class DraftService : BaseContentRestorableEntityService<Draft, DraftModel, DraftFilterModel> , IDraftService
    {
        public DraftService(ContentContext dbContext, IMapper mapper, ILogger<DraftService> logger)
            :base (dbContext, mapper, logger)
        {
            AllSelector = new AllSelector<Guid, Draft>(dbContext, mapper, logger);
        }

        public async Task<OperationResult> SetContentId(Guid id, string contentId)
        {
            var res = await UpdateManager.SetField(id,
                entity => entity.ContentId = contentId
                );
            return res;
        }

        public async Task<RegistryPage<DraftModel>> GetRegistryPage(RegistryQuery<DraftFilterModel> query)
        {
            var page = await AllSelector.GetRegistryPage<DraftFilterModel, DraftModel>(query);
            return page;
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

            //list = list.ApplyStrictFilters(query.Filter);

            list = list.OrderByDescending(m => m.LastUpdated);

            var res = await list.ProjectTo<DraftModel>(mapper.ConfigurationProvider).ToPagedListAsync(query.PageInfo.PageNumber, query.PageInfo.RowsCount);

            var page = new ItemsPage<DraftModel>(res);

            return page;
        }
    }
}
