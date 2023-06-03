using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Entities.Interfaces;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Services;
using SavaDev.Community.Data;
using SavaDev.Community.Data.Contract;
using SavaDev.Community.Data.Contract.Models;
using SavaDev.Community.Data.Entities;

namespace SavaDev.Community.Data.Services
{
    public class SubscriptionService : BaseEntityService<Subscription, SubscriptionModel>, ISubscriptionService
    {
        protected CreateManager<Subscription, SubscriptionModel> CreateManager { get; }

        public SubscriptionService(CommunityContext dbContext, IMapper mapper)
            : base(dbContext, mapper, nameof(SubscriptionService))
        {
            CreateManager = new CreateManager<Subscription, SubscriptionModel>(GetInftrastructure);
        }

        public async Task<OperationResult> Create(SubscriptionModel model)
        {
            return await CreateManager.Create(model);
        }

        public async Task Delete(SubscriptionModel model)
        {
            var entity = await _dbContext.Set<Subscription>()
                .Where(m => m.UserId == model.UserId && m.GroupId == model.GroupId  && !m.IsDeleted)
                .FirstOrDefaultAsync();

            if (entity == null) return;

            entity.IsDeleted = true;
            entity.LastUpdated = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsSubscribed(SubscriptionModel model)
        {
            var check = await _dbContext.Set<Subscription>()
                .Where(m => m.UserId == model.UserId 
                    && m.GroupId == model.GroupId
                    && m.IsApprovedByOwner 
                    && m.IsApprovedByUser
                    && !m.IsDeleted)
                .AnyAsync();

            return check;
        }

        public async Task<IEnumerable<Guid>> GetAllActiveSubscriptions(SubscriptionModel model, string entityName, string module)
        {
            var ids = await _dbContext.Set<Subscription>()
                .Include(m => m.Group)
                .Where(m => m.UserId == model.UserId
                    && m.Group.Entity == entityName
                    && m.Group.Module == module
                    && m.IsApprovedByOwner
                    && m.IsApprovedByUser
                    && !m.IsDeleted)
                .Select(m => m.GroupId)
                .ToListAsync();

            return ids;
        }

        // TODO пейджинг
        public async Task<IEnumerable<string>> GetAllActualSubscriberIds(Guid communityId)
        {
            var list = await _dbContext.Set<Subscription>()
                .Where(m => m.GroupId == communityId && m.IsApprovedByOwner && m.IsApprovedByUser && !m.IsDeleted)
                .Select(m => m.UserId)
                .ToListAsync();

            return list;
        }

        // TODO пейджинг
        public async Task<IEnumerable<string>> GetAllRequestedSubscriberIds(Guid communityId)
        {
            var list = await _dbContext.Set<Subscription>()
                .Where(m => m.GroupId == communityId && !m.IsApprovedByOwner && m.IsApprovedByUser && !m.IsDeleted)
                .Select(m => m.UserId)
                .ToListAsync();

            return list;
        }

        // TODO пейджинг
        public async Task<IEnumerable<string>> GetAllInvitedSubscriberIds(Guid communityId)
        {
            var list = await _dbContext.Set<Subscription>()
                .Where(m => m.GroupId == communityId && m.IsApprovedByOwner && !m.IsApprovedByUser && !m.IsDeleted)
                .Select(m => m.UserId)
                .ToListAsync();

            return list;
        }
    }
}
