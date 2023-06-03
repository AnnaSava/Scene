using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.Community.Data.Entities;
using System.Diagnostics.CodeAnalysis;

namespace SavaDev.Community.Data
{
    public static class CommunityContextBuilderExtensions
    {
        public static void ConfigureContext(
               [NotNull] this ModelBuilder builder,
               BaseDbOptionsExtension options)
        {
            var helper = new TableNameHelper(options.NamingConvention, options.TablePrefix);

            builder.Entity<Entities.Group>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(Entities.Group)));

                b.HasMany(b => b.Lockouts)
                   .WithOne(b => b.Group)
                   .HasForeignKey(b => b.GroupId);

                b.HasMany(b => b.Roles)
                   .WithOne(b => b.Group)
                   .HasForeignKey(b => b.GroupId);

                b.HasMany(b => b.Subscriptions)
                   .WithOne(b => b.Group)
                   .HasForeignKey(b => b.GroupId);
            });

            builder.Entity<Lockout>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(Lockout)));
            });

            builder.Entity<Role>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(Role)));

                b.HasMany(b => b.UserRoles)
                    .WithOne(b => b.Role)
                    .HasForeignKey(b => b.RoleId);

                b.HasMany(b => b.RolePermissions)
                    .WithOne(b => b.Role)
                    .HasForeignKey(b => b.RoleId);
            });

            builder.Entity<RolePermission>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(RolePermission)));

                b.HasKey(b => new { b.RoleId, b.Permission });
            });

            builder.Entity<Subscription>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(Subscription)));
            });

            builder.Entity<UserRole>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(UserRole)));

                b.HasKey(b => new { b.UserId, b.RoleId });
            });
        }
    }
}
