using Framework.Base.DataService.Services;
using Microsoft.EntityFrameworkCore;
using SavaDev.Boxyz.Data.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SavaDev.Boxyz.Data
{
    public class BoxyzContext : FrameworkBaseDbContext
    {
        #region Public DbSets

        public DbSet<UniBoard> UniBoards { get; set; }

        public DbSet<BoardFolk> BoardFolks { get; set; }

        public DbSet<UniShape> UniShapes { get; set; }

        public DbSet<Shape> Shapes { get; set; }

        public DbSet<ShapeFolk> ShapeFolks { get; set; }

        public DbSet<ShapeSide> ShapeSides { get; set; }

        public DbSet<ShapeSideFolk> ShapeSideFolks { get; set; }

        public DbSet<UniBox> UniBoxes { get; set; }

        public DbSet<Box> Boxes { get; set; }

        public DbSet<BoxSide> BoxSides { get; set; }

        public DbSet<ValueSimpleText> ValueSimpleTexts { get; set; }

        public DbSet<ValueText> ValueTexts { get; set; }

        public DbSet<ValueTextFolk> ValueTextFolks { get; set; }

        public DbSet<ValueFloat> ValueFloats { get; set; }

        public DbSet<ValueInteger> ValueIntegers { get; set; }

        public DbSet<ValueCoin> ValueCoins { get; set; }

        public DbSet<Value3dPoint> Value3dPoint { get; set; }

        public DbSet<Value2dPoint> Value2dPoint { get; set; }

        public DbSet<ValueLink> ValueLinks { get; set; }

        public DbSet<ValueDateTime> ValueDateTimes { get; set; }

        public DbSet<ValueFile> ValueFiles { get; set; }

        public DbSet<ValueImage> ValueImages { get; set; }

        #endregion

        public BoxyzContext(DbContextOptions<BoxyzContext> options)
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
