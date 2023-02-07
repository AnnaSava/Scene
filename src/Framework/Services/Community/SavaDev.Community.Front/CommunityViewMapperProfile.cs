using AutoMapper;
using SavaDev.Community.Data.Contract.Models;
using SavaDev.Community.Front.Contract.Models;

namespace SavaDev.Community.Front
{
    public class CommunityViewMapperProfile : Profile
    {
        public CommunityViewMapperProfile()
        {
            CreateMap<CommunityViewModel, GroupModel>();
            CreateMap<GroupModel, CommunityViewModel>();

            CreateMap<CommunitySavingModel, GroupModel>();
            CreateMap<GroupModel, CommunitySavingModel>();

            CreateMap<SubscriptionViewModel, SubscriptionModel>();
            CreateMap<SubscriptionModel, SubscriptionViewModel>();

            CreateMap<SubscriptionFormViewModel, SubscriptionModel>();
            CreateMap<SubscriptionModel, SubscriptionFormViewModel>();
        }
    }
}
