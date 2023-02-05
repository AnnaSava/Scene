using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using System.Diagnostics.CodeAnalysis;

namespace SavaDev.Files.Data
{
    public static class FilesContextBuilderExtensions
    {
        public static void ConfigureContext(
            [NotNull] this ModelBuilder builder,
            BaseDbOptionsExtension options)
        {
            var helper = new TableNameHelper(options.NamingConvention, options.TablePrefix);

            builder.Entity<Entities.File>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(Entities.File)));
            });
        }
    }
}
