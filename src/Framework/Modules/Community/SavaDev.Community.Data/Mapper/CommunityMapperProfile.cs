using AutoMapper;
using SavaDev.Community.Data.Contract.Models;
using SavaDev.Community.Data.Entities;

namespace SavaDev.Community.Data.Mapper
{
    public class CommunityMapperProfile : Profile
    {
        public CommunityMapperProfile()
        {
            CreateMap<Entities.Community, CommunityModel>();
            CreateMap<CommunityModel, Entities.Community>();

            CreateMap<Lockout, LockoutModel>();
            CreateMap<LockoutModel, Lockout>();

            CreateMap<Role, RoleModel>();
            CreateMap<RoleModel, Role>();

            CreateMap<RolePermission, RolePermissionModel>();
            CreateMap<RolePermissionModel, RolePermission>();

            CreateMap<Subscription, SubscriptionModel>();
            CreateMap<SubscriptionModel, Subscription>();

            CreateMap<UserRole, UserRoleModel>();
            CreateMap<UserRoleModel, UserRole>();
        }
    }
}
