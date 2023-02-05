using Framework.Base.DataService.Contract;
using Microsoft.EntityFrameworkCore;
using Sava.Forums.Data;
using Sava.Forums.Data.Entities;
using SavaDev.Base.Data.Context;
using System;
using System.Diagnostics.CodeAnalysis;

namespace SavaDev.Forums.Data
{
    public static class ForumsContextBuilderExtensions
    {
        public static void ConfigureContext(
            [NotNull] this ModelBuilder builder,
            BaseDbOptionsExtension options)
        {
            var helper = new TableNameHelper(options.NamingConvention, options.TablePrefix);

            builder.Entity<Post>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(Post)));
            });

            builder.Entity<Topic>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(Topic)));
            });

            builder.Entity<Forum>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(Forum)));
            });
        }
    }
}
