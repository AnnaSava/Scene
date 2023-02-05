using AutoMapper;
using SavaDev.Files.Data.Contract.Models;

namespace SavaDev.Files.Data
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
