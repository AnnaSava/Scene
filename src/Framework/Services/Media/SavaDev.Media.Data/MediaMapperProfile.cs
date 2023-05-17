using AutoMapper;
using Sava.Media.Data.Contract.Models;
using Sava.Media.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Media.Data
{
    public class MediaMapperProfile : Profile
    {
        public MediaMapperProfile()
        {
            CreateMap<Image, ImageModel>();
            CreateMap<ImageModel, Image>();

            CreateMap<ImageFile, ImageFileModel>();
            CreateMap<ImageFileModel, ImageFile>();

            CreateMap<Image, ImageFormModel>();
            CreateMap<ImageFormModel, Image>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.Created, y => { y.PreCondition(s => s.Id == Guid.Empty); y.MapFrom(s => DateTime.UtcNow); })
                .ForMember(x => x.LastUpdated, y => y.MapFrom(s => DateTime.UtcNow));

            CreateMap<ImageFile, ImageFileFormModel>();
            CreateMap<ImageFileFormModel, ImageFile>();

            CreateMap<Gallery, GalleryModel>();
            CreateMap<GalleryModel, Gallery>();

        }
    }
}
