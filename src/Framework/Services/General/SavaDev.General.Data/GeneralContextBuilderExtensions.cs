using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.General.Data.Entities;
using SavaDev.General.Data.Entities.Parts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.General.Data
{
    public static class GeneralContextBuilderExtensions
    {
        public static void ConfigureContext(
            [NotNull] this ModelBuilder builder,
            BaseDbOptionsExtension options)
        {
            var helper = new TableNameHelper(options.NamingConvention, options.TablePrefix);
            builder.SetTableNames(helper);

            builder.Entity<Permission>(b =>
            {
                b.HasMany(m => m.Cultures)
                    .WithOne(m => m.Permission)
                    .HasForeignKey(m => m.PermissionName)
                    .IsRequired();
            });

            builder.Entity<PermissionCulture>(b =>
            {
                b.HasKey(m => new { m.PermissionName, m.Culture });
            });
        }

        // Если сделать экстеншен-дженериком, не работает создание миграций
        private static void SetTableNames([NotNull] this ModelBuilder builder, TableNameHelper helper)
        {
            builder.Entity<MailTemplate>(b => { b.ToTable(helper.GetTableName(nameof(MailTemplate))); });
            builder.Entity<Permission>(b => { b.ToTable(helper.GetTableName(nameof(Permission))); });
            builder.Entity<PermissionCulture>(b => { b.ToTable(helper.GetTableName(nameof(PermissionCulture))); });
            builder.Entity<LegalDocument>(b => { b.ToTable(helper.GetTableName(nameof(LegalDocument))); });
            builder.Entity<ReservedName>(b => { b.ToTable(helper.GetTableName(nameof(ReservedName))); });
        }
    }
}
