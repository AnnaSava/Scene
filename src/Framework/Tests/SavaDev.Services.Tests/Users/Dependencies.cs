using AutoMapper;
using SavaDev.Users.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Services.Tests.Users
{
    internal class Dependencies
    {
        public static IMapper GetDataMapper() => new MapperConfiguration(opts => { opts.AddProfile<UsersMapperProfile>(); }).CreateMapper();
    }
}
