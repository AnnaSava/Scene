using Microsoft.EntityFrameworkCore;
using SavaDev.System.Data.Contract.Context;
using SavaDev.Base.Data.Context;

namespace SavaDev.System.Data
{
    public class MailTemplateContext : BaseDbContext, IMailTemplateContext
    {
        public DbSet<Entities.MailTemplate> MailTemplates { get; set; }

        public MailTemplateContext(DbContextOptions<MailTemplateContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureContext(_dbOptionsExtension);
        }
    }
}
