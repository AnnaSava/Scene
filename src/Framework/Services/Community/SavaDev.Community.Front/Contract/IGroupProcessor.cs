using SavaDev.Base.Data.Models.Interfaces;
using SavaDev.Base.Data.Services;
using SavaDev.Base.Data.Services.Interfaces;
using SavaDev.Community.Front.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Community.Front.Services.Contract
{
    public interface IGroupProcessor<TUserModel>
    {
        Task<OperationResult> CreateInitialGroup<T, TService>(T resultModel, TService service)
            where T : IHavingGroupModel<long>
            where TService : IHavingGroupService;

        Task<OperationResult> CreateCommunity(GroupFormViewModel model);

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
