using Framework.Base.DataService.Contract;
using Framework.Base.DataService.Contract.Interfaces;
using Microsoft.EntityFrameworkCore;
using Savadev.Content.Data.Entities;
using Savadev.Content.Data.Extentions;

namespace Savadev.Content.Data
{
    public class ContentContext : DbContext, IDbContext
    {
        public DbSet<Draft> Drafts { get; set; }

        public DbSet<Entities.Version> Versions { get; set; }

        public DbSet<Export> Exports { get; set; }

        public DbSet<Import> Imports { get; set; }

        DbContextSettings<ContentContext> Settings;

        public ContentContext(DbContextOptions<ContentContext> options, DbContextSettings<ContentContext> settings)
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
                //options.NamingConvention = Settings.NamingConvention;
            });
        }
    }
}