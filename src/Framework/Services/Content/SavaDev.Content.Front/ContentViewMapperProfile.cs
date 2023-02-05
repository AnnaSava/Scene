using AutoMapper;
using Framework.Base.Service.ListView;
using Savadev.Content.Contract.Models;
using Savadev.Content.Data;
using Savadev.Content.Data.Contract.Models;
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
                .ForMember(x => x.Entity, y => y.MapFrom(s => s.Entity.ToWordListFilterField0()))
                .ForMember(x => x.Module, y => y.MapFrom(s => s.Module.ToWordListFilterField0()))
                .ForMember(x => x.ContentId, y => y.MapFrom(s => s.ContentId.ToWordListFilterField0()))
                .ForMember(x => x.GroupingKey, y => y.MapFrom(s => s.GroupingKey.ToWordListFilterField0()))
                .ForMember(x => x.Owner, y => y.MapFrom(s => s.Owner.ToWordListFilterField0()));

            CreateMap<VersionFilterViewModel, VersionFilterModel>(MemberList.None)
                .ForMember(x => x.Entity, y => y.MapFrom(s => s.Entity.ToWordListFilterField0()))
                .ForMember(x => x.Module, y => y.MapFrom(s => s.Module.ToWordListFilterField0()))
                .ForMember(x => x.ContentId, y => y.MapFrom(s => s.ContentId.ToWordListFilterField0()))
                .ForMember(x => x.Owner, y => y.MapFrom(s => s.Owner.ToWordListFilterField0()));
        }
    }
}
