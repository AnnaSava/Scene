﻿using AutoMapper;
using SavaDev.Base.Data.Registry.Filter;
using SavaDev.General.Data.Contract.Models;
using SavaDev.General.Front.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.General.Front.Mapper
{
    public class GeneralViewMapperProfile : Profile
    {
        public GeneralViewMapperProfile()
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
                .ForMember(x => x.Id, y => y.MapFrom(s => s.Id.ToLongListFilterField()))
                .ForMember(x => x.PermName, y => y.MapFrom(s => s.PermName.ToWordListFilterField()))
                .ForMember(x => x.Title, y => y.MapFrom(s => s.Title.ToWordListFilterField()))
                .ForMember(x => x.Culture, y => y.MapFrom(s => s.Culture.ToWordListFilterField()))
                .ForMember(x => x.Status, y => y.MapFrom(s => new EnumFilterField { Value = s.Status == null ? null : (int)s.Status.Value }));

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
