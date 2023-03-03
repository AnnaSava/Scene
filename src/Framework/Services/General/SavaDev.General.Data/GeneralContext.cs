using Microsoft.EntityFrameworkCore;
using SavaDev.General.Data.Contract.Context;
using SavaDev.Base.Data.Context;
using SavaDev.General.Data.Entities;
using SavaDev.General.Data.Entities.Parts;

namespace SavaDev.General.Data
{
    public class GeneralContext : BaseDbContext, IGeneralContext
    {
        public DbSet<MailTemplate> MailTemplates { get; set; }

        public DbSet<LegalDocument> LegalDocuments { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<PermissionCulture> PermissionCultures { get; set; }

        public DbSet<ReservedName> ReservedNames { get; set; }

        public GeneralContext() { }

        public GeneralContext(DbContextOptions<GeneralContext> options)
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
