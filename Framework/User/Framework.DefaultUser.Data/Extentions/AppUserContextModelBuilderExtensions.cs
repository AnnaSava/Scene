using Framework.Base.DataService.Contract;
using Framework.Base.Types.DataStorage;
using Framework.User.DataService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DefaultUser.Data.Extentions
{
    public static class AppUserContextModelBuilderExtensions
    {
        public static void ConfigureContext(
            [NotNull] this ModelBuilder builder,
            Action<ModelBuilderConfigurationOptions> optionsAction = null)
        {
            var options = new ModelBuilderConfigurationOptions();
            optionsAction?.Invoke(options);

            //TODO отрефакторить для автоматического преобразования naming conventions
            var helper = new TableNameHelper(TableName, options.NamingConvention, options.TablePrefix);

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
            builder.Entity<AppUser>(b => { b.ToTable(helper.GetTableName(nameof(AppUser))); });
            builder.Entity<UserClaim>(b => { b.ToTable(helper.GetTableName(nameof(UserClaim))); });
            builder.Entity<UserLogin>(b => { b.ToTable(helper.GetTableName(nameof(UserLogin))); });
            builder.Entity<UserToken>(b => { b.ToTable(helper.GetTableName(nameof(UserToken))); });
            builder.Entity<AppRole>(b => { b.ToTable(helper.GetTableName(nameof(AppRole))); });
            builder.Entity<RoleClaim>(b => { b.ToTable(helper.GetTableName(nameof(RoleClaim))); });
            builder.Entity<UserRole>(b => { b.ToTable(helper.GetTableName(nameof(UserRole))); });
            builder.Entity<AuthToken>(b => { b.ToTable(helper.GetTableName(nameof(AuthToken))); });
            builder.Entity<Permission>(b => { b.ToTable(helper.GetTableName(nameof(Permission))); });
            builder.Entity<PermissionCulture>(b => { b.ToTable(helper.GetTableName(nameof(PermissionCulture))); });
            builder.Entity<LegalDocument>(b => { b.ToTable(helper.GetTableName(nameof(LegalDocument))); });
            builder.Entity<ReservedName>(b => { b.ToTable(helper.GetTableName(nameof(ReservedName))); });
            builder.Entity<Lockout>(b => { b.ToTable(helper.GetTableName(nameof(Lockout))); });
            builder.Entity<AppUser>(b => { b.ToTable(helper.GetTableName(nameof(AppUser))); });
        }

        // TODO использовать автоматическое преобразование naming conventions вместо этого милого костыля
        private static Dictionary<string, string> TableName => new Dictionary<string, string>
        {
            { nameof(AppUser), "users" },
            { nameof(UserClaim), "user_claims" },
            { nameof(UserLogin), "user_logins" },
            { nameof(UserToken), "user_tokens" },
            { nameof(AppRole), "roles" },
            { nameof(RoleClaim), "role_claims" },
            { nameof(UserRole), "user_roles" },
            { nameof(AuthToken), "auth_tokens" },
            { nameof(Permission), "permissions" },
            { nameof(PermissionCulture), "permission_cultures" },
            { nameof(LegalDocument), "legal_documents" },
            { nameof(ReservedName), "reserved_names" },
            { nameof(Lockout), "lockouts" }
        };
    }
}
