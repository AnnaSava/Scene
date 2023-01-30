using Framework.Base.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Tests.Base.Data
{
    public class ReservedNameTestDbContext : DbContext, IDbContext, IReservedNameContext
    {
        public ReservedNameTestDbContext(DbContextOptions<ReservedNameTestDbContext> options)
            : base(options)
        {

        }

        public DbSet<ReservedName> ReservedNames { get; set; }
    }
}
