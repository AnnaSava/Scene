using Framework.Base.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Interfaces.Context;
using Framework.User.DataService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Tests.Base.Data
{
    public class LegalDocumentTestDbContext : DbContext, IDbContext, ILegalDocumentContext
    {
        public LegalDocumentTestDbContext(DbContextOptions<LegalDocumentTestDbContext> options)
            : base(options)
        {

        }

        public DbSet<LegalDocument> LegalDocuments { get; set; }

    }
}
