using AutoMapper;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;
using SavaDev.Content.Data.Contract;
using SavaDev.Content.Data.Contract.Models;
using SavaDev.Content.Data.Entities;

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

        public async Task<ItemsPage<DraftModel>> GetAll(RegistryQuery query)
        {
            var result = await GetAll(query, _mapper);

            return result;
        }

        private async Task<ItemsPage<DraftModel>> GetAll(RegistryQuery query,
            IMapper mapper)
        {
            var page = await AllSelector.GetItemsPage<DraftModel>(query, ApplyStrictFilter);
            return page;
        }

        #region Private Query

        private IQueryable<Draft> ApplyStrictFilter(IQueryable<Draft> list, RegistryQuery query)
        {
            var filter = query.Filter0 as DraftStrictFilterModel;
            query.Filter0 = null; // TODO менять где-нибудь в другом месте

            if (filter.ContentId == "0") filter.ContentId = null;

            if(filter.OwnerId != null)
            {
                list = list.Where(m => m.OwnerId == filter.OwnerId);
            }

            list = list.Where(m => m.IsDeleted == false 
                && m.Module == filter.Module 
                && m.Entity == filter.Entity
                && m.ContentId == filter.ContentId
                && m.GroupingKey == filter.GroupingKey);

            list = list.OrderByDescending(m => m.LastUpdated);

            return list;
        }

        #endregion
    }
}
