using Framework.MailTemplate.Data.Contract.Context;
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

        public MailTemplateContext(DbContextOptions<MailTemplateContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
