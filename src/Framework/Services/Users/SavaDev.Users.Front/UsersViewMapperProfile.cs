using AutoMapper;
using Framework.Base.Service.ListView;
using Framework.Base.Types.Registry;
using Framework.User.DataService.Contract.Models;
using Framework.User.Service.Contract.Models;
using SavaDev.Users.Data;
using SavaDev.Users.Front.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Users.Front
{
    public class UsersViewMapperProfile : Profile
    {
        public UsersViewMapperProfile()
        {
            CreateMap<LoginViewModel, LoginModel>();

            CreateMap<UserModel, UserViewModel>();
            CreateMap<UserViewModel, UserModel>()
                .ForMember(x => x.IsBanned, y => y.MapFrom(s => s.IsBanned))
                .ForMember(x => x.BanEnd, y => y.MapFrom(s => s.BanEnd));

            CreateMap<UserFormModel, UserFormViewModel>();
            CreateMap<UserFormViewModel, UserFormModel>();

            CreateMap<UserFilterViewModel, UserFilterModel>(MemberList.None)
                .ForMember(x => x.Ids, y => y.MapFrom(s => s.Id.ToLongListFilterField()))
                .ForMember(x => x.Logins, y => y.MapFrom(s => s.Login.ToWordListFilterField()))
                .ForMember(x => x.Emails, y => y.MapFrom(s => s.Email.ToWordListFilterField()))
                .ForMember(x => x.PhoneNumbers, y => y.MapFrom(s => s.PhoneNumber.ToWordListFilterField()))
                .ForMember(x => x.FirstNames, y => y.MapFrom(s => s.FirstName.ToWordListFilterField()))
                .ForMember(x => x.LastNames, y => y.MapFrom(s => s.LastName.ToWordListFilterField()))
                .ForMember(x => x.DisplayNames, y => y.MapFrom(s => s.DisplayName.ToWordListFilterField()));

            //CreateMap<UserRolesFormViewModel, UserRolesModel>();

            CreateMap<RoleFilterViewModel, RoleFilterModel>(MemberList.None)
                .ForMember(x => x.Ids, y => y.MapFrom(s => s.Id.ToLongListFilterField()))
                .ForMember(x => x.Names, y => y.MapFrom(s => s.Name.ToWordListFilterField()));

            CreateMap<RoleModel, RoleViewModel>();

            CreateMap<RoleFormViewModel, RoleModel>(MemberList.None);

            CreateMap<RegisterViewModel, UserFormModel>(MemberList.None)
                .ForMember(x => x.DisplayName, y => y.MapFrom(s => s.Login));

            CreateMap<SignInResultModel<UserModel>, SignInResultViewModel>();
        }
    }
}
