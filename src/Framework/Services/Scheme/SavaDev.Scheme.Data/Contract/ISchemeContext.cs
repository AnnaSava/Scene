using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.Scheme.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Data.Contract
{
    public interface ISchemeContext : IDbContext
    {
        DbSet<Table> Tables { get; set; }

        DbSet<Column> Columns { get; set; }

        DbSet<ColumnConfig> ColumnConfigs { get; set; }

        DbSet<ColumnProperty> ColumnProperties { get; set; }

        DbSet<Filter> Filters { get; set; }
    }
}
