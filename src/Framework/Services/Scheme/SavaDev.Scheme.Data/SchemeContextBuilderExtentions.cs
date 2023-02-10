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

            builder.Entity<Registry>(b =>
            {
                b.HasMany(m => m.Columns).WithOne(m => m.Table).HasForeignKey(m => m.TableId).IsRequired();
                b.HasMany(m => m.Configs).WithOne(m => m.Table).HasForeignKey(m => m.TableId).IsRequired();
                b.HasMany(m => m.Filters).WithOne(m => m.Table).HasForeignKey(m => m.TableId).IsRequired();
            });

            builder.Entity<Column>(b =>
            {
                b.HasMany(m => m.Properties).WithOne(m => m.Column).HasForeignKey(m => m.ColumnId).IsRequired();
                b.HasMany(m => m.Permissions).WithOne(m => m.Column).HasForeignKey(m => m.ColumnId).IsRequired();
            });
        }

        // Если сделать экстеншен-дженериком, не работает создание миграций
        private static void SetTableNames([NotNull] this ModelBuilder builder, TableNameHelper helper)
        {
            builder.Entity<Registry>(b => { b.ToTable(helper.GetTableName(nameof(Registry))); });
            builder.Entity<Column>(b => { b.ToTable(helper.GetTableName(nameof(Column))); });
            builder.Entity<ColumnProperty>(b => { b.ToTable(helper.GetTableName(nameof(ColumnProperty))); });
            builder.Entity<ColumnPermission>(b => { b.ToTable(helper.GetTableName(nameof(ColumnPermission))); });
            builder.Entity<RegistryConfig>(b => { b.ToTable(helper.GetTableName(nameof(RegistryConfig))); });
            builder.Entity<Filter>(b => { b.ToTable(helper.GetTableName(nameof(Filter))); });
        }
    }
}
