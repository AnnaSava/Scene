using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Libs.Tests.Base.Data.Managers.Fake
{
    internal class FakeMapperProfile : Profile
    {
        public FakeMapperProfile()
        {
            CreateMap<FakeEntity, FakeModel>();
            CreateMap<FakeModel, FakeEntity>();
        }
    }
}
