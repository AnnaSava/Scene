using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.Base.User.Data.Entities;
using SavaDev.System.Data.Entities;
using SavaDev.System.Data.Entities.Parts;
using System.Diagnostics.CodeAnalysis;

namespace SavaDev.Users.Data
{
    public static class UsersContextBuilderExtensions
    {
        public static void ConfigureContext(
            [NotNull] this ModelBuilder builder,
            BaseDbOptionsExtension options)
        {
            var helper = new TableNameHelper(options.NamingConvention, options.TablePrefix);
            builder.SetTableNames(helper);

            builder.Entity<UserLogin>(b =>
            {
                b.HasKey(m => new { m.LoginProvider, m.ProviderKey, m.UserId });
            });

            builder.Entity<UserToken>(b =>
            {
                b.HasKey(m => new { m.LoginProvider, m.UserId });
            });

            builder.Entity<UserRole>(b =>
            {
                b.HasKey(m => new { m.UserId, m.RoleId });
            });

            builder.Entity<AuthToken>(b =>
            {
                b.HasIndex(m => m.AuthJti).IsUnique();
                b.HasIndex(m => m.RefreshJti).IsUnique();
            });

            builder.Entity<Permission>(b =>
            {
                b.HasMany(m => m.Cultures)
                    .WithOne(m => m.Permission)
                    .HasForeignKey(m => m.PermissionName)
                    .IsRequired();
            });

            builder.Entity<PermissionCulture>(b =>
            {
                b.HasKey(m => new { m.PermissionName, m.Culture });
            });
        }

        // Если сделать экстеншен-дженериком, не работает создание миграций
        private static void SetTableNames([NotNull] this ModelBuilder builder, TableNameHelper helper)
        {
            // Identity
            builder.Entity<User>(b => { b.ToTable(helper.GetTableName(nameof(User))); });
            builder.Entity<UserClaim>(b => { b.ToTable(helper.GetTableName(nameof(UserClaim))); });
            builder.Entity<UserLogin>(b => { b.ToTable(helper.GetTableName(nameof(UserLogin))); });
            builder.Entity<UserToken>(b => { b.ToTable(helper.GetTableName(nameof(UserToken))); });
            builder.Entity<Role>(b => { b.ToTable(helper.GetTableName(nameof(Role))); });
            builder.Entity<RoleClaim>(b => { b.ToTable(helper.GetTableName(nameof(RoleClaim))); });
            builder.Entity<UserRole>(b => { b.ToTable(helper.GetTableName(nameof(UserRole))); });
            // Custom
            builder.Entity<AuthToken>(b => { b.ToTable(helper.GetTableName(nameof(AuthToken))); });
            builder.Entity<Lockout>(b => { b.ToTable(helper.GetTableName(nameof(Lockout))); });
        }
    }
}
