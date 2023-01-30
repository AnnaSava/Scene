using Framework.Base.DataService.Contract;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.Community.Data.Entities;
using Framework.Community.Data.Extentions;
using Microsoft.EntityFrameworkCore;

namespace Framework.Community.Data
{
    public class CommunityContext : DbContext, IDbContext
    {
        public DbSet<Entities.Community> Communities { get; set; }

        public DbSet<Lockout> Lockouts { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        DbContextSettings<CommunityContext> Settings;

        public CommunityContext(DbContextOptions<CommunityContext> options, DbContextSettings<CommunityContext> settings)
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