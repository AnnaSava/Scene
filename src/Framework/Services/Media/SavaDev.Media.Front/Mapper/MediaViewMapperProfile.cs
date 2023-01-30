using AutoMapper;
using Sava.Media.Contract.Models;
using Sava.Media.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Media.Mapper
{
    public class MediaViewMapperProfile : Profile
    {
        public MediaViewMapperProfile()
        {
            CreateMap<ImageViewModel, ImageModel>();
            CreateMap<ImageModel, ImageViewModel>();

            CreateMap<ImageFileViewModel, ImageFileModel>();
            CreateMap<ImageFileModel, ImageFileViewModel>();

            CreateMap<ImageEmbedFormViewModel, ImageModel>();
            CreateMap<ImageModel, ImageEmbedFormViewModel>();

            CreateMap<GalleryViewModel, GalleryModel>();
            CreateMap<GalleryModel, GalleryViewModel>();
        }
    }
}
