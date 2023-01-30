using Framework.Base.DataService.Contract;
using Framework.Base.DataService.Contract.Interfaces;
using Microsoft.EntityFrameworkCore;
using Sava.Articles.Data.Entities;
using System;

namespace Sava.Articles.Data.Services
{
    public class ArticlesContext : DbContext, IDbContext
    {
        public DbSet<Article> Articles { get; set; }

        public DbSet<ArticleDate> ArticleDates { get; set; }

        public DbSet<ArticlePage> ArticlePages { get; set; }

        public DbSet<ArticleRubric> ArticleRubrics { get; set; }

        public DbSet<ArticleTag> ArticleTags { get; set; }

        public DbSet<Rubric> Rubrics { get; set; }

        public DbSet<Tag> Tags { get; set; } 

        DbContextSettings<ArticlesContext> Settings;

        public ArticlesContext(DbContextOptions<ArticlesContext> options, DbContextSettings<ArticlesContext> settings)
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
