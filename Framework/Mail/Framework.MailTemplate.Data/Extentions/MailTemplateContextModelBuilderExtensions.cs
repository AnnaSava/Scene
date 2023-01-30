using Framework.Base.DataService;
using Framework.Base.DataService.Contract;
using Framework.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MailTemplate.Data.Extentions
{
    public static class MailTemplateContextModelBuilderExtensions
    {
        public static void ConfigureContext(
            [NotNull] this ModelBuilder builder,
            FrameworkDbOptionsExtension options)
        {
            //TODO отрефакторить для автоматического преобразования naming conventions
            var helper = new TableNameHelper(options.NamingConvention, options.TablePrefix);

            builder.Entity<Entities.MailTemplate>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(Entities.MailTemplate)));
            });
        }
    }
}
