using Framework.Base.DataService.Contract;
using Framework.MailTemplate.Data.Contract.Context;
using Framework.MailTemplate.Data.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MailTemplate.Data
{
    public class MailTemplateContext : DbContext, IMailTemplateContext
    {
        public DbSet<Entities.MailTemplate> MailTemplates { get; set; }

        DbContextSettings<MailTemplateContext> Settings;

        public MailTemplateContext(DbContextOptions<MailTemplateContext> options, DbContextSettings<MailTemplateContext > settings)
            : base(options)
        {
            Settings = settings;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureContext(options =>
            {
                options.TablePrefix = Settings.TablePrefix;
                options.NamingConvention = Settings.NamingConvention;
            });
        }
    }
}
