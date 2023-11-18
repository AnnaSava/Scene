using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.Service.Drafts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Service.Drafts
{
    public interface IDraftContext : IDbContext
    {
        public DbSet<Draft> Drafts { get; set; }
    }
}
