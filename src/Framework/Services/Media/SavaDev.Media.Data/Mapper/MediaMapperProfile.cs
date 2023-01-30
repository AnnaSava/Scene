using AutoMapper;
using Sava.Media.Data.Contract.Models;
using Sava.Media.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Media.Data.Mapper
{
    public class MediaMapperProfile : Profile
    {
        public MediaMapperProfile()
        {
            CreateMap<Image, ImageModel>();
            CreateMap<ImageModel, Image>();

            CreateMap<ImageFile, ImageFileModel>();
            CreateMap<ImageFileModel, ImageFile>();

            CreateMap<Gallery, GalleryModel>();
            CreateMap<GalleryModel, Gallery>();
        }
    }
}
