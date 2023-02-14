using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Libs.Tests.Base.Data.Managers.Fake
{
    internal class TestAutoMapperProfile : Profile
    {
        public TestAutoMapperProfile()
        {
            CreateMap<TestEntity, TestModel>();
            CreateMap<TestModel, TestEntity>();
        }
    }
}
