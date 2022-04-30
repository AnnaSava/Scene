using AutoMapper;
using Framework.User.DataService.Contract.Models;
using Framework.User.Service.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Mapper
{
    public class ReservedNameMapperProfile : Profile
    {
        public ReservedNameMapperProfile()
        {
            CreateMap<ReservedNameViewModel, ReservedNameModel>();
            CreateMap<ReservedNameModel, ReservedNameViewModel>();
        }
    }
}
