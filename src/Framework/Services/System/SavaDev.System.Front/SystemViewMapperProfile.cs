using AutoMapper;
using SavaDev.Base.Data.Registry.Filter;
using SavaDev.System.Data.Contract.Models;
using SavaDev.System.Front.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.System.Front.Mapper
{
    public class SystemViewMapperProfile : Profile
    {
        public SystemViewMapperProfile()
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

            CreateMap<LegalDocumentViewModel, LegalDocumentModel>();
            CreateMap<LegalDocumentModel, LegalDocumentViewModel>();

            CreateMap<LegalDocumentFormViewModel, LegalDocumentModel>();
            CreateMap<LegalDocumentModel, LegalDocumentFormViewModel>();

            CreateMap<LegalDocumentFilterViewModel, LegalDocumentFilterModel>(MemberList.None)
                .ForMember(x => x.PermName, y => y.MapFrom(s => s.PermName.ToWordListFilterField()))
                .ForMember(x => x.Title, y => y.MapFrom(s => s.Title.ToWordListFilterField()))
                .ForMember(x => x.Culture, y => y.MapFrom(s => s.Culture.ToWordListFilterField()));

            CreateMap<MailTemplateViewModel, MailTemplateModel>();
            CreateMap<MailTemplateModel, MailTemplateViewModel>();

            CreateMap<MailTemplateFormViewModel, MailTemplateModel>();
            CreateMap<MailTemplateModel, MailTemplateFormViewModel>();

            CreateMap<MailTemplateFilterViewModel, MailTemplateFilterModel>(MemberList.None)
                .ForMember(x => x.PermName, y => y.MapFrom(s => s.PermName.ToWordListFilterField()))
                .ForMember(x => x.Title, y => y.MapFrom(s => s.Title.ToWordListFilterField()))
                .ForMember(x => x.Culture, y => y.MapFrom(s => s.Culture.ToWordListFilterField()));
        }
    }
}
