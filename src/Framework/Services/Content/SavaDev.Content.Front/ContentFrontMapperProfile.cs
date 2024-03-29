﻿using AutoMapper;
using SavaDev.Content.Contract.Models;
using SavaDev.Content.Data;
using SavaDev.Content.Data.Contract.Models;
using SavaDev.Base.Data.Registry.Filter;

namespace SavaDev.Content.Front
{
    public class ContentFrontMapperProfile : Profile
    {
        public ContentFrontMapperProfile()
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
