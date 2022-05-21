using AutoMapper;
using Framework.MailTemplate.Data.Contract.Models;

namespace Framework.MailTemplate.Data.Mapper
{
    public class MailTemplateDataMapperProfile : Profile
    {
        public MailTemplateDataMapperProfile()
        {
            CreateMap<Data.Entities.MailTemplate, MailTemplateModel>();
            CreateMap<MailTemplateModel, Data.Entities.MailTemplate>();
        }
    }
}
