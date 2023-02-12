using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.System.Data.Entities;
using SavaDev.System.Data.Entities.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Tests.Base.Data
{
    public class PermissionTestDbContext : DbContext, IDbContext
    {
        public PermissionTestDbContext(DbContextOptions<PermissionTestDbContext> options)
            : base(options)
        {

        }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<PermissionCulture> PermissionCultures { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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
    }
}
