using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Libs.Tests.Base.Data.Services.Fake
{
    internal class FakeMapperProfile : Profile
    {
        public FakeMapperProfile()
        {
            CreateMap<FakeDocument, FakeDocumentModel>();
            CreateMap<FakeDocumentModel, FakeDocument>()
                .ForMember(x => x.PermName, y => y.Condition(c => c.Id == 0)) // TODO вынести в общий кастомный маппер
                .ForMember(x => x.Culture, y => y.Condition(c => c.Id == 0))
                .ForMember(x => x.Status, y => y.Ignore())
                .ForMember(x => x.IsDeleted, y => y.Ignore())
                .ForMember(x => x.Created, y => y.Ignore())
                .ForMember(x => x.LastUpdated, y => y.Ignore());
        }
    }
}
