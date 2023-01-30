using AutoMapper;
using Framework.MailTemplate.Data.Contract.Models;

namespace Framework.MailTemplate.Data.Mapper
{
    public class MailTemplateDataMapperProfile : Profile
    {
        public MailTemplateDataMapperProfile()
        {
            CreateMap<Data.Entities.MailTemplate, MailTemplateModel>();
            CreateMap<MailTemplateModel, Data.Entities.MailTemplate>()
                .ForMember(x => x.PermName, y => y.Condition(c => c.Id == 0))
                .ForMember(x => x.Culture, y => y.Condition(c => c.Id == 0))
                .ForMember(x => x.Status, y => y.Ignore())
                .ForMember(x => x.IsDeleted, y => y.Ignore())
                .ForMember(x => x.Created, y => y.Ignore())
                .ForMember(x => x.LastUpdated, y => y.Ignore());
        }
    }
}
