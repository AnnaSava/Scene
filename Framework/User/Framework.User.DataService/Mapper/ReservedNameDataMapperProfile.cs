using AutoMapper;
using Framework.User.DataService.Contract.Models;
using Framework.User.DataService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Mapper
{
    public class ReservedNameDataMapperProfile : Profile
    {
        public ReservedNameDataMapperProfile()
        {
            CreateMap<ReservedName, ReservedNameModel>();
            CreateMap<ReservedNameModel, ReservedName>();
        }
    }
}
