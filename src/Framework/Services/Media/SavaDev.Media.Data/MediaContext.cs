using Framework.Base.DataService.Contract;
using Framework.Base.DataService.Contract.Interfaces;
using Microsoft.EntityFrameworkCore;
using Sava.Media.Data.Entities;

namespace Sava.Media.Data
{
    public class MediaContext : DbContext, IDbContext
    {
        public DbSet<Image> Images { get; set; }
        public DbSet<ImageFile> ImageFiles { get; set; }
        public DbSet<Gallery> Galleries { get; set; }

        DbContextSettings<MediaContext> Settings;

        public MediaContext(DbContextOptions<MediaContext> options, DbContextSettings<MediaContext> settings)
           : base(options)
        {
            Settings = settings;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureContext(options =>
            {
                options.TablePrefix = Settings.TablePrefix;
                //options.NamingConvention = Settings.NamingConvention;
            });
        }
    }
}