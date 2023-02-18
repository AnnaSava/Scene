using Microsoft.EntityFrameworkCore;
using SavaDev.System.Data.Contract.Context;
using SavaDev.Base.Data.Context;
using SavaDev.System.Data.Entities;
using SavaDev.System.Data.Entities.Parts;

namespace SavaDev.System.Data
{
    public class SystemContext : BaseDbContext, ISystemContext
        //IPermissionContext,
        //ILegalDocumentContext,
        //IReservedNameContext
        //IMailTemplateContext
    {
        public DbSet<MailTemplate> MailTemplates { get; set; }

        public DbSet<LegalDocument> LegalDocuments { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<PermissionCulture> PermissionCultures { get; set; }

        public DbSet<ReservedName> ReservedNames { get; set; }

        public SystemContext() { }

        public SystemContext(DbContextOptions<SystemContext> options)
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
