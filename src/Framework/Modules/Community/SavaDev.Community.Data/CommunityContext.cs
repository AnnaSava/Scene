using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.Community.Data.Entities;

namespace SavaDev.Community.Data
{
    public class CommunityContext : DbContext, IDbContext
    {
        public DbSet<Entities.Community> Communities { get; set; }

        public DbSet<Lockout> Lockouts { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public CommunityContext(DbContextOptions<CommunityContext> options)
           : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.ConfigureContext(options =>
            //{
            //   // options.TablePrefix = Settings.TablePrefix;
            //    //options.NamingConvention = Settings.NamingConvention;
            //});
        }
    }
}