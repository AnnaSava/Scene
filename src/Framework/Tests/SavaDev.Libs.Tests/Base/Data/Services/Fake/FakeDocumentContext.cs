using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Libs.Tests.Base.Data.Services.Fake
{
    internal class FakeDocumentContext : DbContext, IDbContext
    {
        public DbSet<FakeDocument> FakeDocuments  { get; set; }

        public FakeDocumentContext() { }

        public FakeDocumentContext(DbContextOptions<FakeDocumentContext> options)
            : base(options)
        {

        }
    }
}