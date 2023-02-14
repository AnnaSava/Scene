using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Libs.Tests.Base.Data.Managers.Fake
{
    internal class TestDbContext : DbContext, IDbContext
    {
        public DbSet<TestEntity> TestEntities { get; set; }

        public TestDbContext() { }

        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {

        }
    }
}