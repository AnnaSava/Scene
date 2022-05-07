using Framework.Base.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Interfaces.Context;
using Framework.User.DataService.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Services
{
    // Конечная реализация для проектов, где нужен минимальный функционал для пользователей
    public class FrameworkUserDbContext : IdentityDbContext<FrameworkUser, FrameworkRole, long, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>,
        IDbContext,
        IUserContext<FrameworkUser>,
        IRoleContext<FrameworkRole, RoleClaim>,
        IReservedNameContext,
        IConsentContext,
        IAuthTokenContext,
        IPermissionContext
    {
        public FrameworkUserDbContext(DbContextOptions<FrameworkUserDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<PermissionCulture> PermissionCultures { get; set; }

        public DbSet<ReservedName> ReservedNames { get; set; }

        public DbSet<Consent> Consents { get; set; }

        public DbSet<AuthToken> AuthTokens { get; set; }

        public DbSet<Lockout> Lockouts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FrameworkUser>(b =>
            {
                b.ToTable("Users");
            });

            builder.Entity<UserClaim>(b =>
            {
                b.ToTable("UserClaims");
            });

            builder.Entity<UserLogin>(b =>
            {
                b.HasKey(m => new { m.LoginProvider, m.ProviderKey, m.UserId });
                b.ToTable("UserLogins");
            });

            builder.Entity<UserToken>(b =>
            {
                b.HasKey(m => new { m.LoginProvider, m.UserId });
                b.ToTable("UserTokens");
            });

            builder.Entity<FrameworkRole>(b =>
            {
                b.ToTable("Roles");
            });

            builder.Entity<RoleClaim>(b =>
            {
                b.ToTable("RoleClaims");
            });

            builder.Entity<UserRole>(b =>
            {
                b.HasKey(m => new { m.UserId, m.RoleId });
                b.ToTable("UserRoles");
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

            builder.Entity<AuthToken>(b =>
            {
                b.HasIndex(m => m.AuthJti).IsUnique();
                b.HasIndex(m => m.RefreshJti).IsUnique();
            });
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
