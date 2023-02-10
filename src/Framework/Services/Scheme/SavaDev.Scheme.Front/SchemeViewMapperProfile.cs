using AutoMapper;
using SavaDev.Scheme.Contract.Models;
using SavaDev.Scheme.Data.Contract;
using SavaDev.Scheme.Data.Contract.Models;
using SavaDev.Scheme.Data.Entities;
using SavaDev.Scheme.Front.Contract.Models;

namespace SavaDev.Scheme.Front
{
    public class SchemeViewMapperProfile : Profile
    {
        public SchemeViewMapperProfile()
        {
            CreateMap<ColumnViewModel, ColumnModel>();
            CreateMap<ColumnModel, ColumnViewModel>();

            CreateMap<RegistryViewModel, RegistryModel>();
            CreateMap<RegistryModel, RegistryViewModel>();

            CreateMap<ColumnPropertyViewModel, ColumnPropertyModel>();
            CreateMap<ColumnPropertyModel, ColumnPropertyViewModel>();

            CreateMap<ColumnPermissionViewModel, ColumnPermissionModel>();
            CreateMap<ColumnPermissionModel, ColumnPermissionViewModel>();

            CreateMap<RegistryConfigViewModel, RegistryConfigModel>();
            CreateMap<RegistryConfigModel, RegistryConfigViewModel>();

            CreateMap<FilterViewModel, FilterModel>();
            CreateMap<FilterModel, FilterViewModel>();
        }
    }
}
