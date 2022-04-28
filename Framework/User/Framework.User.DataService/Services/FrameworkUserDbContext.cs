using Framework.Base.DataService.Contract.Interfaces;
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
    public class FrameworkUserDbContext<TUser, TRole>
        : IdentityDbContext<TUser, TRole, long, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>,
        IDbContext
        where TUser : IdentityUser<long>
        where TRole : IdentityRole<long>
    {
        public DbSet<ReservedName> ReservedNames { get; set; }

        public DbSet<Consent> Consents { get; set; }

        public DbSet<AuthToken> AuthTokens { get; set; }

        public DbSet<Lockout> Lockouts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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
