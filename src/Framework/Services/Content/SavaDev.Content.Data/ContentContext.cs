using Microsoft.EntityFrameworkCore;
using SavaDev.Content.Data.Entities;
using SavaDev.Base.Data.Context;
using SavaDev.Content.Data;

namespace SavaDev.Content.Data
{
    public class ContentContext : BaseDbContext, IDbContext
    {
        public DbSet<Draft> Drafts { get; set; }

        public DbSet<Entities.Version> Versions { get; set; }

        public DbSet<Export> Exports { get; set; }

        public DbSet<Import> Imports { get; set; }

        public ContentContext(DbContextOptions<ContentContext> options)
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