using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.General.Data.Entities;
using SavaDev.General.Data.Entities.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.General.Data.Contract.Context
{
    public interface IGeneralContext : IDbContext
    {
        public DbSet<MailTemplate> MailTemplates { get; set; }

        public DbSet<LegalDocument> LegalDocuments { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<PermissionCulture> PermissionCultures { get; set; }

        public DbSet<ReservedName> ReservedNames { get; set; }
    }
}
