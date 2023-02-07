using AutoMapper;
using Framework.User.DataService.Contract;
using SavaDev.Base.Data.Services;
using SavaDev.Community.Data;
using SavaDev.Community.Data.Contract;
using SavaDev.Community.Data.Contract.Models;
using SavaDev.Community.Front.Contract.Models;
using SavaDev.Community.Front.Services.Contract;
using SavaDev.Community.Service.Contract;

namespace SavaDev.Community.Front.Services
{
    public class CommunityProcessor<TUserModel> : ICommunityProcessor<TUserModel>
    {
        IUserSearchService<TUserModel> _userService;

        IGroupService _communityService;
        ISubscriptionService _subscriptionService;
        ILockoutService _lockoutService;
        IMapper _mapper;

        public CommunityProcessor(
            IGroupService communityService,
            ISubscriptionService subscriptionService,
            ILockoutService lockoutService,
            IUserSearchService<TUserModel> userService,
            IMapper mapper)
        {
             _communityService = communityService;
            _subscriptionService = subscriptionService;
            _lockoutService = lockoutService;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<OperationResult> CreateCommunity(CommunitySavingModel model)
        {
            var newModel = _mapper.Map<GroupModel>(model);
            var resModel = await _communityService.Create(newModel);
            return resModel;
           // return new OperationResult<CommunityViewModel>(resModel.Rows, _mapper.Map<CommunityViewModel>(resModel.Model));
        }

        public async Task CreateSubscription(SubscriptionFormViewModel model)
        {
            var saving = _mapper.Map<SubscriptionModel>(model);

            await _subscriptionService.Create(saving);
        }

        public async Task DeleteSubscription(SubscriptionFormViewModel model)
        {
            var saving = _mapper.Map<SubscriptionModel>(model);

            await _subscriptionService.Delete(saving);
        }

        public async Task<bool> IsSubscribed(string userId, Guid communityId)
        {
            var model = new SubscriptionModel
            {
                UserId = userId,
                CommunityId = communityId,
            };

            return await _subscriptionService.IsSubscribed(model);
        }

        public async Task<IEnumerable<Guid>> GetAllActiveSubscriptions(string userId, string entityName, string module)
        {
            var model = new SubscriptionModel
            {
                UserId = userId,
            };

            return await _subscriptionService.GetAllActiveSubscriptions(model, entityName, module);
        }

        // TODO пейджинг
        public async Task<IEnumerable<TUserModel>> GetAllActualSubscribers(Guid communityId)
        {
            var subscriberIds = await _subscriptionService.GetAllActualSubscriberIds(communityId);
            var users = await _userService.GetAllByIds(subscriberIds);
            return users;
        }

        // TODO пейджинг
        public async Task<IEnumerable<TUserModel>> GetAllRequestedSubscribers(Guid communityId)
        {
            var subscriberIds = await _subscriptionService.GetAllRequestedSubscriberIds(communityId);
            var users = await _userService.GetAllByIds(subscriberIds);
            return users;
        }

        // TODO пейджинг
        public async Task<IEnumerable<TUserModel>> GetAllInvitedSubscribers(Guid communityId)
        {
            var subscriberIds = await _subscriptionService.GetAllInvitedSubscriberIds(communityId);
            var users = await _userService.GetAllByIds(subscriberIds);
            return users;
        }

        // TODO пейджинг
        public async Task<IEnumerable<TUserModel>> GetAllActualLockouts(Guid communityId)
        {
            var lockoutIds = await _lockoutService.GetAllActualIds(communityId);
            var users = await _userService.GetAllByIds(lockoutIds);
            return users;
        }
    }
}
