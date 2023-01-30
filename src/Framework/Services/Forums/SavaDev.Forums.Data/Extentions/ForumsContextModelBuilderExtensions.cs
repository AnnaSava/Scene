

using Framework.Base.DataService.Contract;
using Microsoft.EntityFrameworkCore;
using Sava.Forums.Data.Entities;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Sava.Forums.Data
{
    public static class ForumsContextModelBuilderExtensions
    {

        public static void ConfigureContext(
            [NotNull] this ModelBuilder builder,
            Action<ModelBuilderConfigurationOptions> optionsAction = null)
        {
            var options = new ModelBuilderConfigurationOptions();
            optionsAction?.Invoke(options);

            var helper = new TableNameHelper(options.NamingConvention, options.TablePrefix);

            // TODO
            //builder.Entity<Post>(b =>
            //{
            //    b.ToTable(helper.GetTableName(nameof(Post)));
            //});

            //builder.Entity<Topic>(b =>
            //{
            //    b.ToTable(helper.GetTableName(nameof(Topic)));
            //});

            //builder.Entity<Forum>(b =>
            //{
            //    b.ToTable(helper.GetTableName(nameof(Forum)));
            //});
        }
    }
}
