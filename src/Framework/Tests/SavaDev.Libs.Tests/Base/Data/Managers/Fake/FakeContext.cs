using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Libs.Tests.Base.Data.Managers.Fake
{
    internal class FakeContext : BaseDbContext, IDbContext
    {
        public DbSet<FakeEntity> TestEntities { get; set; }

        public FakeContext() { }

        public FakeContext(DbContextOptions<FakeContext> options)
            : base(options)
        {

        }
    }
}