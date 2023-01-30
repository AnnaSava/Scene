using AutoMapper;
using Sava.Files.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Files.Data.Mapper
{
    public class FilesMapperProfile : Profile
    {
        public FilesMapperProfile()
        {
            CreateMap<Entities.File, FileModel>();
            CreateMap<FileModel, Entities.File>();
        }
    }
}
