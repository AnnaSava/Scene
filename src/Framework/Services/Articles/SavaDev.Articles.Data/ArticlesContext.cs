using Microsoft.EntityFrameworkCore;
using Sava.Articles.Data.Entities;
using SavaDev.Articles.Data;
using SavaDev.Base.Data.Context;
using System;

namespace Sava.Articles.Data.Services
{
    public class ArticlesContext : BaseDbContext, IDbContext
    {
        public DbSet<Article> Articles { get; set; }

        public DbSet<ArticleDate> ArticleDates { get; set; }

        public DbSet<ArticlePage> ArticlePages { get; set; }

        public DbSet<ArticleRubric> ArticleRubrics { get; set; }

        public DbSet<ArticleTag> ArticleTags { get; set; }

        public DbSet<Rubric> Rubrics { get; set; }

        public DbSet<Tag> Tags { get; set; } 

        public ArticlesContext(DbContextOptions<ArticlesContext> options)
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
