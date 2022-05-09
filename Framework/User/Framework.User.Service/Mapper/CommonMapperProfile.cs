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
    public class CommonMapperProfile : Profile
    {
        public CommonMapperProfile()
        {
            CreateMap<ReservedNameViewModel, ReservedNameModel>();
            CreateMap<ReservedNameModel, ReservedNameViewModel>();

            CreateMap<ReservedNameFilterViewModel, ReservedNameFilterModel>(MemberList.None)
                .ForMember(x => x.Text, y => y.MapFrom(s => s.Text.ToWordListFilterField()));

            CreateMap<ReservedNameFormViewModel, ReservedNameModel>(MemberList.None);


            CreateMap<PermissionFilterViewModel, PermissionFilterModel>(MemberList.None)
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name.ToWordListFilterField()));

            CreateMap<PermissionViewModel, PermissionModel>();
            CreateMap<PermissionModel, PermissionViewModel>();
        }
    }
}
