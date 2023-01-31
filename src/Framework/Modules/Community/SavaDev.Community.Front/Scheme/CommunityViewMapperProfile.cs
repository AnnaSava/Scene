using AutoMapper;
using SavaDev.Community.Data.Contract.Models;
using SavaDev.Community.Front.Contract.Models;

namespace SavaDev.Community.Front.Scheme
{
    public class CommunityViewMapperProfile : Profile
    {
        public CommunityViewMapperProfile()
        {
            CreateMap<CommunityViewModel, CommunityModel>();
            CreateMap<CommunityModel, CommunityViewModel>();

            CreateMap<CommunitySavingModel, CommunityModel>();
            CreateMap<CommunityModel, CommunitySavingModel>();

            CreateMap<SubscriptionViewModel, SubscriptionModel>();
            CreateMap<SubscriptionModel, SubscriptionViewModel>();

            CreateMap<SubscriptionFormViewModel, SubscriptionModel>();
            CreateMap<SubscriptionModel, SubscriptionFormViewModel>();
        }
    }
}
