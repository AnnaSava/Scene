using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.Boxyz.Data.Entities;
using System.Diagnostics.CodeAnalysis;

namespace SavaDev.Boxyz.Data
{
    public static class BoxyzContextBuilderExtensions
    {
        public static void ConfigureContext(
            [NotNull] this ModelBuilder builder,
            BaseDbOptionsExtension options)
        {
            var helper = new TableNameHelper(options.NamingConvention, options.TablePrefix);

            builder.Entity<UniBoard>(b =>
            {
                b.HasMany(m => m.ChildUniBoards).WithOne(m => m.ParentUniBoard).HasForeignKey(m => m.ParentUniBoardId);
                b.HasMany(m => m.Editions).WithOne(m => m.Uniq).HasForeignKey(m => m.UniqId).IsRequired();
                b.HasMany(m => m.UniShapes).WithOne(m => m.UniBoard).HasForeignKey(m => m.UniBoardId).IsRequired();
            });

            builder.Entity<Board>(b =>
            {
                b.HasMany(m => m.Folks).WithOne(m => m.Content).HasForeignKey(m => m.ContentId).IsRequired();
                b.HasMany(m => m.Shapes).WithOne(m => m.Board).HasForeignKey(m => m.BoardId).IsRequired();
            });

            builder.Entity<UniShape>(b =>
            {
                b.HasMany(m => m.Editions).WithOne(m => m.Uniq).HasForeignKey(m => m.UniqId).IsRequired();
                b.HasMany(m => m.UniBoxes).WithOne(m => m.UniShape).HasForeignKey(m => m.UniShapeId).IsRequired();
            });

            builder.Entity<Shape>(b =>
            {
                b.HasMany(m => m.Folks).WithOne(m => m.Content).HasForeignKey(m => m.ContentId).IsRequired();
                b.HasMany(m => m.Sides).WithOne(m => m.Shape).HasForeignKey(m => m.ShapeId).IsRequired();
                b.HasMany(m => m.Boxes).WithOne(m => m.Shape).HasForeignKey(m => m.ShapeId).IsRequired();
            });

            builder.Entity<ShapeSide>(b =>
            {
                b.HasMany(m => m.Stamps).WithOne(m => m.ShapeSide).HasForeignKey(m => m.ShapeSideId).IsRequired();
                b.HasMany(m => m.Folks).WithOne(m => m.Content).HasForeignKey(m => m.ContentId).IsRequired();
            });

            builder.Entity<UniBox>(b =>
            {
                b.HasMany(m => m.Editions).WithOne(m => m.Uniq).HasForeignKey(m => m.UniqId).IsRequired();
            });

            builder.Entity<Box>(b =>
            {
                b.HasMany(m => m.Sides).WithOne(m => m.Box).HasForeignKey(m => m.BoxId).IsRequired();
            });

            builder.Entity<BoxSide>(b =>
            {
                b.HasMany(m => m.SimpleTexts).WithOne(m => m.BoxSide).HasForeignKey(m => m.BoxSideId).IsRequired();
                b.HasMany(m => m.Texts).WithOne(m => m.BoxSide).HasForeignKey(m => m.BoxSideId).IsRequired();
                b.HasMany(m => m.Floats).WithOne(m => m.BoxSide).HasForeignKey(m => m.BoxSideId).IsRequired();
                b.HasMany(m => m.Integers).WithOne(m => m.BoxSide).HasForeignKey(m => m.BoxSideId).IsRequired();
                b.HasMany(m => m.Coins).WithOne(m => m.BoxSide).HasForeignKey(m => m.BoxSideId).IsRequired();
                b.HasMany(m => m.Points2d).WithOne(m => m.BoxSide).HasForeignKey(m => m.BoxSideId).IsRequired();
                b.HasMany(m => m.Points3d).WithOne(m => m.BoxSide).HasForeignKey(m => m.BoxSideId).IsRequired();
                b.HasMany(m => m.Links).WithOne(m => m.BoxSide).HasForeignKey(m => m.BoxSideId).IsRequired();
                b.HasMany(m => m.Files).WithOne(m => m.BoxSide).HasForeignKey(m => m.BoxSideId).IsRequired();
                b.HasMany(m => m.Images).WithOne(m => m.BoxSide).HasForeignKey(m => m.BoxSideId).IsRequired();
                b.HasMany(m => m.Dates).WithOne(m => m.BoxSide).HasForeignKey(m => m.BoxSideId).IsRequired();
            });

            builder.Entity<ValueText>(b =>
            {
                b.HasMany(m => m.Folks).WithOne(m => m.Content).HasForeignKey(m => m.ContentId).IsRequired();
            });

            builder.Entity<ValueLink>(b =>
            {
                b.HasOne(m => m.LinkedBox).WithMany(m => m.LinkedBoxes).HasForeignKey(m => m.LinkedBoxId).IsRequired();
            });

            builder.Entity<BoardFolk>().HasKey(m => new { m.Culture, m.ContentId });
            builder.Entity<ShapeFolk>().HasKey(m => new { m.Culture, m.ContentId });
            builder.Entity<ShapeSideFolk>().HasKey(m => new { m.Culture, m.ContentId });
            builder.Entity<ValueTextFolk>().HasKey(m => new { m.Culture, m.ContentId });
        }

    }
}
