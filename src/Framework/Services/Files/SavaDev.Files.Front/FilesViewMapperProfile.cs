using AutoMapper;
using SavaDev.Files.Data.Contract.Models;
using SavaDev.Files.Front.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Files.Front
{
    public class FilesViewMapperProfile : Profile
    {
        public FilesViewMapperProfile()
        {
            CreateMap<FileViewModel, FileModel>();
            CreateMap<FileModel, FileViewModel>();

            CreateMap<FileFilterViewModel, FileFilterModel>();
            CreateMap<FileFilterModel, FileFilterViewModel>();
        }
    }
}
