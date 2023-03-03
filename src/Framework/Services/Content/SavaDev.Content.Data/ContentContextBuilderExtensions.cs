using Microsoft.EntityFrameworkCore;
using SavaDev.Content.Data.Entities;
using SavaDev.Base.Data.Context;
using System.Diagnostics.CodeAnalysis;

namespace SavaDev.Content.Data
{
    public static class ContentContextBuilderExtensions
    {
        public static void ConfigureContext(
               [NotNull] this ModelBuilder builder,
               BaseDbOptionsExtension options)
        {
            var helper = new TableNameHelper(options.NamingConvention, options.TablePrefix);

            builder.Entity<Draft>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(Draft)));
            });

            builder.Entity<Entities.Version>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(Entities.Version)));
            });

            builder.Entity<Export>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(Export)));
            });

            builder.Entity<Import>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(Import)));
            });
        }
    }
}
