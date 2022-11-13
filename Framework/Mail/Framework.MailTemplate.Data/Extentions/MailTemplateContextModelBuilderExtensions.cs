using Framework.Base.DataService.Contract;
using Framework.Base.Types.DataStorage;
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
            Action<ModelBuilderConfigurationOptions> optionsAction = null)
        {
            var options = new ModelBuilderConfigurationOptions();
            optionsAction?.Invoke(options);

            //TODO отрефакторить для автоматического преобразования naming conventions
            var helper = new TableNameHelper(TableName, options.NamingConvention, options.TablePrefix);

            builder.Entity<Entities.MailTemplate>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(Entities.MailTemplate)));
            });
        }

        // TODO использовать автоматическое преобразование naming conventions вместо этого милого костыля
        private static Dictionary<string, string> TableName => new Dictionary<string, string>
        {
            { nameof(Entities.MailTemplate), "mail_templates" }
        };
    }
}
