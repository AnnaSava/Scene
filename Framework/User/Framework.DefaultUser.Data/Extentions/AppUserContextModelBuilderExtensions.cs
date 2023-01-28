using Framework.Base.DataService.Contract;
using Framework.Base.Types.DataStorage;
using Framework.Helpers;
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
        }
    }
}
