using AutoMapper;
using Framework.Base.Service.ListView;
using Savadev.Content.Contract.Models;
using Savadev.Content.Data;
using Savadev.Content.Data.Contract.Models;
using SavaDev.Base.Data.Registry.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Content.Front
{
    public class ContentViewMapperProfile : Profile
    {
        public ContentViewMapperProfile()
        {
            CreateMap<DraftViewModel, DraftModel>();
            CreateMap<DraftModel, DraftViewModel>();

            CreateMap<VersionViewModel, VersionModel>();
            CreateMap<VersionModel, VersionViewModel>();

            CreateMap<DraftFilterViewModel, DraftFilterModel>(MemberList.None)
                .ForMember(x => x.Entity, y => y.MapFrom(s => s.Entity.ToWordListFilterField()))
                .ForMember(x => x.Module, y => y.MapFrom(s => s.Module.ToWordListFilterField()))
                .ForMember(x => x.ContentId, y => y.MapFrom(s => s.ContentId.ToWordListFilterField()))
                .ForMember(x => x.GroupingKey, y => y.MapFrom(s => s.GroupingKey.ToWordListFilterField()))
                .ForMember(x => x.Owner, y => y.MapFrom(s => s.Owner.ToWordListFilterField()));

            CreateMap<VersionFilterViewModel, VersionFilterModel>(MemberList.None)
                .ForMember(x => x.Entity, y => y.MapFrom(s => s.Entity.ToWordListFilterField()))
                .ForMember(x => x.Module, y => y.MapFrom(s => s.Module.ToWordListFilterField()))
                .ForMember(x => x.ContentId, y => y.MapFrom(s => s.ContentId.ToWordListFilterField()))
                .ForMember(x => x.Owner, y => y.MapFrom(s => s.Owner.ToWordListFilterField()));
        }
    }
}
