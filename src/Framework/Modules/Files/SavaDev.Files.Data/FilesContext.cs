using Framework.Base.DataService.Contract;
using Framework.Base.DataService.Contract.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Files.Data
{
    public class FilesContext : DbContext, IDbContext
    {
        public DbSet<Entities.File> Files { get; set; }

        DbContextSettings<FilesContext> Settings;

        public FilesContext(DbContextOptions<FilesContext> options, DbContextSettings<FilesContext> settings)
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
