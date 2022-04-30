using AutoMapper;
using Framework.Base.Service.ListView;
using Framework.User.DataService.Contract.Models;
using Framework.User.Service.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Mapper
{
    public class FrameworkUserMapperProfile : Profile
    {
        public FrameworkUserMapperProfile()
        {
            CreateMap<FrameworkUserModel, FrameworkUserViewModel>();
            CreateMap<FrameworkUserViewModel, FrameworkUserModel>()
                .ForMember(x => x.IsBanned, y => y.MapFrom(s => s.IsBanned))
                .ForMember(x => x.BanEnd, y => y.MapFrom(s => s.BanEnd));

            CreateMap<FrameworkUserFormModel, FrameworkUserFormViewModel>();
            CreateMap<FrameworkUserFormViewModel, FrameworkUserFormModel>();

            CreateMap<FrameworkUserFilterViewModel, FrameworkUserFilterModel>(MemberList.None)
                .ForMember(x => x.Ids, y => y.MapFrom(s => s.Id.ToLongListFilterField()))
                .ForMember(x => x.Logins, y => y.MapFrom(s => s.Login.ToWordListFilterField()))
                .ForMember(x => x.Emails, y => y.MapFrom(s => s.Email.ToWordListFilterField()))
                .ForMember(x => x.PhoneNumbers, y => y.MapFrom(s => s.PhoneNumber.ToWordListFilterField()))
                .ForMember(x => x.FirstNames, y => y.MapFrom(s => s.FirstName.ToWordListFilterField()))
                .ForMember(x => x.LastNames, y => y.MapFrom(s => s.LastName.ToWordListFilterField()))
                .ForMember(x => x.DisplayNames, y => y.MapFrom(s => s.DisplayName.ToWordListFilterField()));

            CreateMap<UserRolesFormViewModel, UserRolesModel>();

            CreateMap<FrameworkRoleFilterViewModel, FrameworkRoleFilterModel>(MemberList.None)
                .ForMember(x => x.Ids, y => y.MapFrom(s => s.Id.ToLongListFilterField()))
                .ForMember(x => x.Names, y => y.MapFrom(s => s.Name.ToWordListFilterField()));

            CreateMap<FrameworkRoleModel, FrameworkRoleViewModel>();

            CreateMap<FrameworkRoleFormViewModel, FrameworkRoleModel>(MemberList.None);

            CreateMap<FrameworkRegisterViewModel, FrameworkUserFormModel>(MemberList.None)
                .ForMember(x => x.DisplayName, y => y.MapFrom(s => s.Login));
        }
    }
}
