using Framework.Base.DataService.Contract;
using Framework.Community.Data.Entities;
using Framework.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Community.Data.Extentions
{
    public static class CommunityContextModelBuilderExtensions
    {
        public static void ConfigureContext(
               [NotNull] this ModelBuilder builder,
               Action<ModelBuilderConfigurationOptions> optionsAction = null)
        {
            var options = new ModelBuilderConfigurationOptions();
            optionsAction?.Invoke(options);

            var helper = new TableNameHelper(options.NamingConvention, options.TablePrefix);

            builder.Entity<Entities.Community>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(Entities.Community)));

                b.HasMany(b => b.Lockouts)
                   .WithOne(b => b.Community)
                   .HasForeignKey(b => b.CommunityId);

                b.HasMany(b => b.Roles)
                   .WithOne(b => b.Community)
                   .HasForeignKey(b => b.CommunityId);

                b.HasMany(b => b.Subscriptions)
                   .WithOne(b => b.Community)
                   .HasForeignKey(b => b.CommunityId);
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
