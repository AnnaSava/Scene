using AutoMapper;
using Framework.Community.Data;
using Framework.Community.Data.Contract.Models;
using Framework.Community.Service.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Community.Service.Mapper
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
