using Microsoft.EntityFrameworkCore;
using Sava.Articles.Data.Entities;
using SavaDev.Base.Data.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Articles.Data
{
    public static class ArticlesContextModelBuilderExtensions
    {
        public static void ConfigureContext(
            [NotNull] this ModelBuilder builder,
            Action<ModelBuilderConfigurationOptions> optionsAction = null)
        {
            var options = new ModelBuilderConfigurationOptions();
            optionsAction?.Invoke(options);

            var helper = new TableNameHelper(options.NamingConvention, options.TablePrefix);

            builder.Entity<Article>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(Article)));

                b.HasMany(m => m.Dates)
                    .WithOne(m => m.Article)
                    .HasForeignKey(m => m.ArticleId)
                    .IsRequired();

                b.HasMany(m => m.Pages)
                    .WithOne(m => m.Article)
                    .HasForeignKey(m => m.ArticleId)
                    .IsRequired();

                b.HasMany(m => m.Tags)
                    .WithOne(m => m.Article)
                    .HasForeignKey(m => m.ArticleId)
                    .IsRequired();
            });

            builder.Entity<ArticleRubric>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(ArticleRubric)));

                b.HasKey(m => new { m.ArticleId, m.RubricId });

                b.HasOne(m => m.Article)
                    .WithMany(m => m.Rubrics)
                    .HasForeignKey(m => m.ArticleId)
                    .IsRequired();

                b.HasOne(m => m.Rubric)
                    .WithMany(m => m.Articles)
                    .HasForeignKey(m => m.RubricId)
                    .IsRequired();
            });

            builder.Entity<ArticleDate>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(ArticleDate)));

                b.HasKey(m => new { m.ArticleId, m.Date });
            });

            builder.Entity<ArticlePage>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(ArticlePage)));

                b.HasKey(m => new { m.ArticleId, m.PageNumber });
            });

            builder.Entity<ArticleTag>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(ArticleTag)));

                b.HasKey(m => new { m.ArticleId, m.TagId });

                b.HasOne(m => m.Article)
                    .WithMany(m => m.Tags)
                    .HasForeignKey(m => m.ArticleId)
                    .IsRequired();

                b.HasOne(m => m.Tag)
                    .WithMany(m => m.Articles)
                    .HasForeignKey(m => m.TagId)
                    .IsRequired();
            });

            builder.Entity<Rubric>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(Rubric)));
            });

            builder.Entity<Tag>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(Tag)));
            });
        }
    }
}
