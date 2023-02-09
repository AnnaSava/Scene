using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.System.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Tests.Base.Data
{
    public class LegalDocumentTestDbContext : DbContext, IDbContext
    {
        public LegalDocumentTestDbContext(DbContextOptions<LegalDocumentTestDbContext> options)
            : base(options)
        {

        }

        public DbSet<LegalDocument> LegalDocuments { get; set; }

    }
}
