using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Services;
using SavaDev.Community.Data;
using SavaDev.Community.Data.Contract;
using SavaDev.Community.Data.Contract.Models;
using SavaDev.Community.Data.Entities;

namespace SavaDev.Community.Data.Services
{
    public class SubscriptionService : BaseEntityService<Subscription, SubscriptionModel>, ISubscriptionService
    {
        public SubscriptionService(CommunityContext dbContext, IMapper mapper)
            : base(dbContext, mapper, nameof(SubscriptionService))
        {

        }

        public async Task<SubscriptionModel> Create(SubscriptionModel model)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(SubscriptionModel model)
        {
            var entity = await _dbContext.Set<Subscription>()
                .Where(m => m.UserId == model.UserId && m.CommunityId == model.CommunityId  && !m.IsDeleted)
                .FirstOrDefaultAsync();

            if (entity == null) return;

            entity.IsDeleted = true;
            entity.LastUpdated = DateTime.Now;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsSubscribed(SubscriptionModel model)
        {
            var check = await _dbContext.Set<Subscription>()
                .Where(m => m.UserId == model.UserId 
                    && m.CommunityId == model.CommunityId
                    && m.IsApprovedByOwner 
                    && m.IsApprovedByUser
                    && !m.IsDeleted)
                .AnyAsync();

            return check;
        }

        public async Task<IEnumerable<Guid>> GetAllActiveSubscriptions(SubscriptionModel model, string entityName, string module)
        {
            var ids = await _dbContext.Set<Subscription>()
                .Include(m => m.Community)
                .Where(m => m.UserId == model.UserId
                    && m.Community.Entity == entityName
                    && m.Community.Module == module
                    && m.IsApprovedByOwner
                    && m.IsApprovedByUser
                    && !m.IsDeleted)
                .Select(m => m.CommunityId)
                .ToListAsync();

            return ids;
        }

        // TODO пейджинг
        public async Task<IEnumerable<string>> GetAllActualSubscriberIds(Guid communityId)
        {
            var list = await _dbContext.Set<Subscription>()
                .Where(m => m.CommunityId == communityId && m.IsApprovedByOwner && m.IsApprovedByUser && !m.IsDeleted)
                .Select(m => m.UserId)
                .ToListAsync();

            return list;
        }

        // TODO пейджинг
        public async Task<IEnumerable<string>> GetAllRequestedSubscriberIds(Guid communityId)
        {
            var list = await _dbContext.Set<Subscription>()
                .Where(m => m.CommunityId == communityId && !m.IsApprovedByOwner && m.IsApprovedByUser && !m.IsDeleted)
                .Select(m => m.UserId)
                .ToListAsync();

            return list;
        }

        // TODO пейджинг
        public async Task<IEnumerable<string>> GetAllInvitedSubscriberIds(Guid communityId)
        {
            var list = await _dbContext.Set<Subscription>()
                .Where(m => m.CommunityId == communityId && m.IsApprovedByOwner && !m.IsApprovedByUser && !m.IsDeleted)
                .Select(m => m.UserId)
                .ToListAsync();

            return list;
        }
    }
}
