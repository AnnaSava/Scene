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
        public DbSet<Table> Tables { get; set; }

        public DbSet<Column> Columns { get; set; }

        public DbSet<ColumnProperty> ColumnProperties { get; set; }
    }
}
