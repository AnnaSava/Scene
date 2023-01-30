using AutoMapper;
using Framework.Base.Types.Registry;
using Framework.Community.Data;
using Framework.Community.Data.Contract;
using Framework.Community.Data.Contract.Models;
using Framework.Community.Data.Services;
using Framework.Community.Service.Contract;
using Framework.Community.Service.Contract.Models;
using Framework.User.DataService.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Community.Service.Services
{
    public class CommunityProcessor<TUserModel> : ICommunityProcessor<TUserModel>
    {
        IUserSearchService<TUserModel> _userService;

        CommunityService _communityService;
        ISubscriptionService _subscriptionService;
        ILockoutService _lockoutService;
        IMapper _mapper;

        public CommunityProcessor(
            ICommunityService communityService,
            ISubscriptionService subscriptionService,
            ILockoutService lockoutService,
            IUserSearchService<TUserModel> userService,
            IMapper mapper)
        {
            //todo _communityService = communityService;
            _subscriptionService = subscriptionService;
            _lockoutService = lockoutService;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<OperationResult<CommunityViewModel>> CreateCommunity(CommunitySavingModel model)
        {
            var newModel = _mapper.Map<CommunityModel>(model);
            var resModel = await _communityService.Create(newModel);
            return new OperationResult<CommunityViewModel>(resModel.Rows, _mapper.Map<CommunityViewModel>(resModel.Model));
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
