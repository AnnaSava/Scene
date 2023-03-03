using AutoMapper;
using SavaDev.Content.Data;
using SavaDev.Content.Data.Entities;

namespace SavaDev.Content.Data
{
    public class ContentMapperProfile : Profile
    {
        public ContentMapperProfile()
        {
            CreateMap<Draft, DraftModel>();
            CreateMap<DraftModel, Draft>();

            CreateMap<Entities.Version, VersionModel>();
            CreateMap<VersionModel, Entities.Version >();

            CreateMap<Export, ExportModel>();
            CreateMap<ExportModel, Export>();

            CreateMap<Import, ImportModel>();
            CreateMap<ImportModel, Import>();
        }
    }
}
