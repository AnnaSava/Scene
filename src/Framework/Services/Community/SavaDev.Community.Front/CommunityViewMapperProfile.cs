using AutoMapper;
using SavaDev.Community.Data.Contract.Models;
using SavaDev.Community.Front.Contract.Models;

namespace SavaDev.Community.Front
{
    public class CommunityViewMapperProfile : Profile
    {
        public CommunityViewMapperProfile()
        {
            CreateMap<GroupViewModel, GroupModel>();
            CreateMap<GroupModel, GroupViewModel>();

            CreateMap<GroupFormViewModel, GroupModel>();
            CreateMap<GroupModel, GroupFormViewModel>();

            CreateMap<SubscriptionViewModel, SubscriptionModel>();
            CreateMap<SubscriptionModel, SubscriptionViewModel>();

            CreateMap<SubscriptionFormViewModel, SubscriptionModel>();
            CreateMap<SubscriptionModel, SubscriptionFormViewModel>();

            CreateMap<GroupFilterViewModel, GroupFilterModel>();
            CreateMap<GroupFilterModel, GroupFilterViewModel>();
        }
    }
}
