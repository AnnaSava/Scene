using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.Scheme.Data.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Data
{
    public static class SchemeContextBuilderExtentions
    {
        public static void ConfigureContext(
            [NotNull] this ModelBuilder builder,
            BaseDbOptionsExtension options)
        {
            var helper = new TableNameHelper(options.NamingConvention, options.TablePrefix);
            builder.SetTableNames(helper);

            builder.Entity<Table>(b =>
            {
                b.HasMany(m => m.Columns).WithOne(m => m.Table).HasForeignKey(m => m.TableId).IsRequired();
            });

            builder.Entity<Column>(b =>
            {
                b.HasMany(m => m.Properties).WithOne(m => m.Column).HasForeignKey(m => m.ColumnId).IsRequired();
            });
        }

        // Если сделать экстеншен-дженериком, не работает создание миграций
        private static void SetTableNames([NotNull] this ModelBuilder builder, TableNameHelper helper)
        {
            builder.Entity<Table>(b => { b.ToTable(helper.GetTableName(nameof(Table))); });
            builder.Entity<Column>(b => { b.ToTable(helper.GetTableName(nameof(Column))); });
            builder.Entity<ColumnProperty>(b => { b.ToTable(helper.GetTableName(nameof(ColumnProperty))); });
        }
    }
}
