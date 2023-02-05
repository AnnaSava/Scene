using Framework.Helpers;
using Microsoft.EntityFrameworkCore;
using Sava.Media.Data.Entities;
using SavaDev.Base.Data.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Media.Data
{
    public static class MediaContextBuilderExtensions
    {
        public static void ConfigureContext(
            [NotNull] this ModelBuilder builder,
            BaseDbOptionsExtension options)
        {
            var helper = new TableNameHelper(options.NamingConvention, options.TablePrefix);

            builder.Entity<Image>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(Image)));

                b.HasMany(m => m.Files)
                    .WithOne(m => m.Image)
                    .HasForeignKey(m => m.ImageId)
                    .IsRequired();
            });

            builder.Entity<ImageFile>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(ImageFile)));

                b.HasKey(m => new { m.ImageId, m.FileId });
            });

            builder.Entity<Gallery>(b =>
            {
                b.ToTable(helper.GetTableName(nameof(Gallery)));

                b.HasMany(m => m.Images)
                    .WithOne(m => m.Gallery)
                    .HasForeignKey(m => m.GalleryId)
                    .IsRequired();
            });
        }
    }
}
