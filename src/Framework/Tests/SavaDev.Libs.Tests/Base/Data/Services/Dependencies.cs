using AutoMapper;
using SavaDev.Libs.Tests.Base.Data.Services.Fake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Libs.Tests.Base.Data.Services
{
    internal class Dependencies
    {
        public static IMapper GetDataMapper() => new MapperConfiguration(opts => { opts.AddProfile<FakeMapperProfile>(); }).CreateMapper();
    }
}
