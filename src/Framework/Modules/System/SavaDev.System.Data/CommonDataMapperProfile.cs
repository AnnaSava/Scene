using AutoMapper;
using SavaDev.System.Data.Contract.Models;
using SavaDev.System.Data.Entities;

namespace SavaDev.System.Data
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
