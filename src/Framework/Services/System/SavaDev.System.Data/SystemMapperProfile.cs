using AutoMapper;
using SavaDev.System.Data.Contract.Models;
using SavaDev.System.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.System.Data
{
    public class SystemMapperProfile : Profile
    {
        public SystemMapperProfile()
        {
            CreateMap<LegalDocument, LegalDocumentModel>();
            CreateMap<LegalDocumentModel, LegalDocument>()
                .ForMember(x => x.PermName, y => y.Condition(c => c.Id == 0))
                .ForMember(x => x.Culture, y => y.Condition(c => c.Id == 0))
                .ForMember(x => x.Status, y => y.Ignore())
                .ForMember(x => x.IsDeleted, y => y.Ignore())
                .ForMember(x => x.Created, y => y.Ignore())
                .ForMember(x => x.LastUpdated, y => y.Ignore());

            CreateMap<MailTemplate, MailTemplateModel>();
            CreateMap<MailTemplateModel, MailTemplate>()
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
