using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.System.Data.Entities;
using SavaDev.System.Data.Entities.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.System.Data.Contract.Context
{
    public interface ISystemContext : IDbContext
    {
        public DbSet<MailTemplate> MailTemplates { get; set; }

        public DbSet<LegalDocument> LegalDocuments { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<PermissionCulture> PermissionCultures { get; set; }

        public DbSet<ReservedName> ReservedNames { get; set; }
    }
}
