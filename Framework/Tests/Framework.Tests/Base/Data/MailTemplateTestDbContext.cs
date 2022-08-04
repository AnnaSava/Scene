using Framework.Base.DataService.Contract.Interfaces;
using Framework.MailTemplate.Data.Contract.Context;
using Framework.User.DataService.Contract.Interfaces.Context;
using Framework.User.DataService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Tests.Base.Data
{
    public class MailTemplateTestDbContext : DbContext, IDbContext, IMailTemplateContext
    {
        public MailTemplateTestDbContext(DbContextOptions<MailTemplateTestDbContext> options)
            : base(options)
        {

        }

        public DbSet<MailTemplate.Data.Entities.MailTemplate> MailTemplates { get; set; }

    }
}
