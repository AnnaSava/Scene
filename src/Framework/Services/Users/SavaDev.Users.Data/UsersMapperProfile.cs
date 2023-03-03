using AutoMapper;
using SavaDev.Users.Data.Contract.Models;
using SavaDev.Users.Data.Entities;

namespace SavaDev.Users.Data
{
    public class UsersMapperProfile : Profile
    {
        public UsersMapperProfile()
        {
            CreateMap<User, UserModel>()
                .ForMember(x => x.Login, y => y.MapFrom(s => s.UserName))
                .ForMember(x => x.IsBanned, y => y.MapFrom(s => s.LockoutEnabled))
                .ForMember(x => x.BanEnd, y => y.MapFrom(s => s.LockoutEnd));
            CreateMap<UserModel, User>()
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

            CreateMap<User, UserFormModel>()
                .ForMember(x => x.Login, y => y.MapFrom(s => s.UserName));
            CreateMap<UserFormModel, User>(MemberList.Source)
                .ForMember(x => x.UserName, y => y.MapFrom(s => s.Login))
                .ForMember(x => x.LockoutEnabled, y => y.MapFrom(s => s.IsBanned))
                .ForMember(x => x.LockoutEnd, y => y.MapFrom(s => s.BanEnd))
                .ForMember(x => x.NormalizedUserName, y => y.Ignore())
                .ForMember(x => x.NormalizedEmail, y => y.Ignore())
                .ForMember(x => x.PasswordHash, y => y.Ignore())
                .ForMember(x => x.SecurityStamp, y => y.Ignore())
                .ForMember(x => x.ConcurrencyStamp, y => y.Ignore())
                .ForMember(x => x.TwoFactorEnabled, y => y.Ignore())
                .ForMember(x => x.AccessFailedCount, y => y.Ignore())
                .ForMember(x => x.LastUpdated, y => y.Ignore());

            CreateMap<Role, RoleModel>(MemberList.None);
            CreateMap<RoleModel, Role>()
                .ForMember(x => x.ConcurrencyStamp, y => y.Ignore())
                .ForMember(x => x.NormalizedName, y => y.Ignore());
        }
    }
}
