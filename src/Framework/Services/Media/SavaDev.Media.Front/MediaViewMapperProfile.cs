using AutoMapper;
using Sava.Media.Contract.Models;
using Sava.Media.Data.Contract.Models;
using SavaDev.Media.Data.Contract.Models;
using SavaDev.Media.Front.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Media.Front
{
    public class MediaViewMapperProfile : Profile
    {
        public MediaViewMapperProfile()
        {
            CreateMap<ImageViewModel, ImageModel>();
            CreateMap<ImageModel, ImageViewModel>();

            CreateMap<ImageFileViewModel, ImageFileModel>();
            CreateMap<ImageFileModel, ImageFileViewModel>();

            CreateMap<ImageFormModel, ImageViewModel>();

            CreateMap<ImageFileFormModel, ImageFileViewModel>();

            CreateMap<ImageFilterViewModel, ImageFilterModel>();
            CreateMap<ImageFilterModel, ImageFilterViewModel>();

            CreateMap<ImageEmbedFormViewModel, ImageModel>();
            CreateMap<ImageModel, ImageEmbedFormViewModel>();

            CreateMap<GalleryViewModel, GalleryModel>();
            CreateMap<GalleryModel, GalleryViewModel>();

            CreateMap<GalleryFilterViewModel, GalleryFilterModel>();
            CreateMap<GalleryFilterModel, GalleryFilterViewModel>();
        }
    }
}
