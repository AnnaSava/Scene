using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.Scheme.Data.Contract;
using SavaDev.Scheme.Data.Entities;

namespace SavaDev.Scheme.Data
{
    public class SchemeContext : BaseDbContext, ISchemeContext
    {
        public SchemeContext(DbContextOptions<SchemeContext> options)
            : base(options)
        {
        }

        public DbSet<Table> Tables { get; set; }

        public DbSet<Column>  Columns { get; set; }

        public DbSet<ColumnProperty> ColumnProperties { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureContext(_dbOptionsExtension);
        }
    }
}