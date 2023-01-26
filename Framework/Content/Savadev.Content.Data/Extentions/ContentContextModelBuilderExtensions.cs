using Framework.Base.DataService.Contract;
using Framework.Helpers;
using Microsoft.EntityFrameworkCore;
using Savadev.Content.Data.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savadev.Content.Data.Extentions
{
    public static class ContentContextModelBuilderExtensions
    {
        public static void ConfigureContext(
               [NotNull] this ModelBuilder builder,
               Action<ModelBuilderConfigurationOptions> optionsAction = null)
        {
            var options = new ModelBuilderConfigurationOptions();
            optionsAction?.Invoke(options);

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
