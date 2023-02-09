using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.System.Data.Entities;

namespace Framework.Tests.Base.Data
{
    public class MailTemplateTestDbContext : DbContext, IDbContext
    {
        public MailTemplateTestDbContext(DbContextOptions<MailTemplateTestDbContext> options)
            : base(options)
        {

        }

        public DbSet<MailTemplate> MailTemplates { get; set; }

    }
}
