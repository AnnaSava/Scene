using AutoMapper;
using Savadev.Content.Data.Entities;

namespace Savadev.Content.Data.Mapper
{
    public class ContentMapperProfile : Profile
    {
        public ContentMapperProfile()
        {
            CreateMap<Draft, DraftModel>();
            CreateMap<DraftModel, Draft>();

            CreateMap<Entities.Version, VersionModel>();
            CreateMap<VersionModel, Entities.Version>();

            CreateMap<Export, ExportModel>();
            CreateMap<ExportModel, Export>();

            CreateMap<Import, ImportModel>();
            CreateMap<ImportModel, Import>();
        }
    }
}
