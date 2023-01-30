using Framework.Helpers;
using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Files.Data
{
    public static class FileContextModelBuilderExtensions
    {
        public static void ConfigureContext(
            [NotNull] this ModelBuilder builder,
            Action<ModelBuilderConfigurationOptions> optionsAction = null)
        {
            var options = new ModelBuilderConfigurationOptions();
            optionsAction?.Invoke(options);

            var helper = new TableNameHelper( options.NamingConvention, options.TablePrefix);

            builder.Entity<Entities.File>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(Entities.File)));
            });
        }
    }
}
