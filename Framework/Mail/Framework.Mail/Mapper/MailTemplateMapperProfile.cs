using AutoMapper;
using Framework.Base.Service.ListView;
using Framework.MailTemplate;
using Framework.MailTemplate.Data.Contract.Models;

namespace Framework.MailTemplate.Service.Mapper
{
    public class MailTemplateMapperProfile : Profile
    {
        public MailTemplateMapperProfile()
        {
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
