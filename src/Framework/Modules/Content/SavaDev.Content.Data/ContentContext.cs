using Microsoft.EntityFrameworkCore;
using Savadev.Content.Data.Entities;
using Savadev.Content.Data.Extentions;
using SavaDev.Base.Data.Context;

namespace Savadev.Content.Data
{
    public class ContentContext : DbContext, IDbContext
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

            builder.ConfigureContext(options =>
            {
                //options.TablePrefix = Settings.TablePrefix;
                //options.NamingConvention = Settings.NamingConvention;
            });
        }
    }
}