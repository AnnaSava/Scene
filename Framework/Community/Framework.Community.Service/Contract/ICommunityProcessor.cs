using Framework.Base.Types.View;
using Framework.Community.Service.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Community.Service.Contract
{
    public interface ICommunityProcessor<TUserModel>
    {
        Task<OperationResult<CommunityViewModel>> CreateCommunity(CommunitySavingModel model);

        Task CreateSubscription(SubscriptionFormViewModel model);

        Task DeleteSubscription(SubscriptionFormViewModel model);

        Task<bool> IsSubscribed(string userId, Guid communityId);

        Task<IEnumerable<Guid>> GetAllActiveSubscriptions(string userId, string entityName, string module);

        Task<IEnumerable<TUserModel>> GetAllActualSubscribers(Guid communityId);

        Task<IEnumerable<TUserModel>> GetAllRequestedSubscribers(Guid communityId);

        Task<IEnumerable<TUserModel>> GetAllInvitedSubscribers(Guid communityId);

        Task<IEnumerable<TUserModel>> GetAllActualLockouts(Guid communityId);
    }
}
