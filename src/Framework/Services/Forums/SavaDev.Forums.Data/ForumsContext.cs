using Framework.Base.DataService.Contract;
using Framework.Base.DataService.Contract.Interfaces;
using Microsoft.EntityFrameworkCore;
using Sava.Forums.Data.Entities;

namespace Sava.Forums.Data.Services
{
    public class ForumsContext : DbContext, IDbContext
    {
        public DbSet<Forum> Forums { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Post> Posts { get; set; }

        DbContextSettings<ForumsContext> Settings;

        public ForumsContext(DbContextOptions<ForumsContext> options, DbContextSettings<ForumsContext> settings)
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
