using Microsoft.EntityFrameworkCore;
using Sava.Forums.Data.Entities;
using SavaDev.Base.Data.Context;
using SavaDev.Forums.Data;

namespace Sava.Forums.Data.Services
{
    public class ForumsContext : BaseDbContext, IDbContext
    {
        public DbSet<Forum> Forums { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Post> Posts { get; set; }

        public ForumsContext(DbContextOptions<ForumsContext> options)
           : base(options)
        {
             
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureContext(_dbOptionsExtension);
        }
    }
}
