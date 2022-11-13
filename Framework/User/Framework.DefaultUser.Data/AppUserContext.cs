using Framework.Base.DataService.Contract;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.DefaultUser.Data.Extentions;
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
    public class AppUserContext : IdentityDbContext<AppUser, AppRole, long, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>,
        IDbContext,
        IUserContext<AppUser>,
        IRoleContext<AppRole, RoleClaim>,
        IReservedNameContext,
        IAuthTokenContext,
        IPermissionContext,
        ILegalDocumentContext
    {
        DbContextSettings<AppUserContext> Settings;

        public AppUserContext(DbContextOptions<AppUserContext> options, DbContextSettings<AppUserContext> settings)
            : base(options)
        {
            Settings = settings;
        }

        public DbSet<LegalDocument> LegalDocuments { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<PermissionCulture> PermissionCultures { get; set; }

        public DbSet<ReservedName> ReservedNames { get; set; }

        public DbSet<AuthToken> AuthTokens { get; set; }

        public DbSet<Lockout> Lockouts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureContext(options =>
            {
                options.TablePrefix = Settings.TablePrefix;
                options.NamingConvention = Settings.NamingConvention;
            });
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
