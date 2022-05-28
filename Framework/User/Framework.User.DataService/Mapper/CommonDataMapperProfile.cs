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
    public class CommonDataMapperProfile : Profile
    {
        public CommonDataMapperProfile()
        {
            CreateMap<LegalDocument, LegalDocumentModel>();
            CreateMap<LegalDocumentModel, LegalDocument>()
                .ForMember(x => x.PermName, y => y.Condition(c => c.Id == 0))
                .ForMember(x => x.Culture, y => y.Condition(c => c.Id == 0))
                .ForMember(x => x.Status, y => y.Ignore())
                .ForMember(x => x.IsDeleted, y => y.Ignore())
                .ForMember(x => x.Created, y => y.Ignore())
                .ForMember(x => x.LastUpdated, y => y.Ignore());

            CreateMap<ReservedName, ReservedNameModel>();
            CreateMap<ReservedNameModel, ReservedName>();

            CreateMap<Permission, PermissionModel>();
            CreateMap<PermissionModel, Permission>();
        }
    }
}
