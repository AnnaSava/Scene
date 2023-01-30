using AutoMapper;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Services;
using Framework.Community.Data.Contract;
using Framework.Community.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Community.Data.Services
{
    public class SubscriptionService : BaseEntityService<Subscription, SubscriptionModel>, ISubscriptionService
    {
        public SubscriptionService(CommunityContext dbContext, IMapper mapper)
            : base(dbContext, mapper, nameof(SubscriptionService))
        {

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
