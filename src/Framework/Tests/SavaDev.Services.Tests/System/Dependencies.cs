using AutoMapper;
using SavaDev.System.Data;
using SavaDev.Users.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Services.Tests.System
{
    internal class Dependencies
    {
        public static IMapper GetDataMapper() => new MapperConfiguration(opts => { opts.AddProfile<SystemMapperProfile>(); }).CreateMapper();
    }
}
