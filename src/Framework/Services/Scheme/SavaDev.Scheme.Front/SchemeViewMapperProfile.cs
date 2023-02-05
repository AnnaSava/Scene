using AutoMapper;
using SavaDev.Scheme.Contract.Models;
using SavaDev.Scheme.Front.Contract.Models;

namespace SavaDev.Scheme.Front
{
    public class SchemeViewMapperProfile : Profile
    {
        public SchemeViewMapperProfile()
        {
            CreateMap<ColumnViewModel, ColumnModel>();
            CreateMap<ColumnModel, ColumnViewModel>();

            CreateMap<TableViewModel, TableModel>();
            CreateMap<TableModel, TableViewModel>();

            CreateMap<ColumnPropertyViewModel, ColumnPropertyModel>();
            CreateMap<ColumnPropertyModel, ColumnPropertyViewModel>();
        }
    }
}
