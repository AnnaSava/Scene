using AutoMapper;
using SavaDev.Scheme.Contract.Models;
using SavaDev.Scheme.Data.Contract;
using SavaDev.Scheme.Data.Contract.Models;
using SavaDev.Scheme.Data.Entities;

namespace SavaDev.Scheme.Data
{
    public class SchemeMapperProfile : Profile
    {
        public SchemeMapperProfile()
        {
            CreateMap<Column, ColumnModel>();
            CreateMap<ColumnModel, Column>();

            CreateMap<Table, TableModel>();
            CreateMap<TableModel, Table>();

            CreateMap<ColumnProperty, ColumnPropertyModel>();
            CreateMap<ColumnPropertyModel, ColumnProperty>();

            CreateMap<ColumnPermission, ColumnPermissionModel>();
            CreateMap<ColumnPermissionModel, ColumnPermission>();

            CreateMap<TableConfig, TableConfigModel>();
            CreateMap<TableConfigModel, TableConfig>();
        }
    }
}
