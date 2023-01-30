using AutoMapper;
using Framework.User.DataService.Contract.Models;
using Framework.User.DataService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Mapper
{
    public class AppUserDataMapperProfile : Profile
    {
        public AppUserDataMapperProfile()
        {
            CreateMap<AppUser, AppUserModel>()
                .ForMember(x => x.Login, y => y.MapFrom(s => s.UserName))
                .ForMember(x => x.IsBanned, y => y.MapFrom(s => s.LockoutEnabled))
                .ForMember(x => x.BanEnd, y => y.MapFrom(s => s.LockoutEnd));
            CreateMap<AppUserModel, AppUser>()
                .ForMember(x => x.UserName, y => y.MapFrom(s => s.Login))
                .ForMember(x => x.LockoutEnabled, y => y.MapFrom(s => s.IsBanned))
                .ForMember(x => x.LockoutEnd, y => y.MapFrom(s => s.BanEnd))
                .ForMember(x => x.NormalizedUserName, y => y.Ignore())
                .ForMember(x => x.NormalizedEmail, y => y.Ignore())
                .ForMember(x => x.PasswordHash, y => y.Ignore())
                .ForMember(x => x.SecurityStamp, y => y.Ignore())
                .ForMember(x => x.ConcurrencyStamp, y => y.Ignore())
                .ForMember(x => x.TwoFactorEnabled, y => y.Ignore())
                .ForMember(x => x.AccessFailedCount, y => y.Ignore());
            //.ForMember(x => x.UserClaims, y => y.Ignore());

            CreateMap<AppUser, AppUserFormModel>()
                .ForMember(x => x.Login, y => y.MapFrom(s => s.UserName));
            CreateMap<AppUserFormModel, AppUser>(MemberList.Source)
                .ForMember(x => x.UserName, y => y.MapFrom(s => s.Login));

            CreateMap<AppRole, AppRoleModel>(MemberList.None);
            CreateMap<AppRoleModel, AppRole>()
                .ForMember(x => x.ConcurrencyStamp, y => y.Ignore())
                .ForMember(x => x.NormalizedName, y => y.Ignore());
        }
    }
}
