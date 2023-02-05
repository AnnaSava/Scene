using Microsoft.EntityFrameworkCore;
using Sava.Media.Data.Entities;
using SavaDev.Base.Data.Context;
using SavaDev.Media.Data;

namespace Sava.Media.Data
{
    public class MediaContext : BaseDbContext, IDbContext
    {
        public DbSet<Image> Images { get; set; }
        public DbSet<ImageFile> ImageFiles { get; set; }
        public DbSet<Gallery> Galleries { get; set; }

        public MediaContext(DbContextOptions<MediaContext> options)
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