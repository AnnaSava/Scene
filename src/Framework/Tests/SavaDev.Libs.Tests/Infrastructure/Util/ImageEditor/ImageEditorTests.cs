using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Registry.Filter;
using SavaDev.Infrastructure.Util.ImageEditor;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Libs.Tests.Infrastructure.Util.ImageEditor
{
    public class ImageEditorTests : IDisposable
    {
        private SavaDev.Infrastructure.Util.ImageEditor.ImageEditor _imageEditor;

        public ImageEditorTests()
        {
            _imageEditor = new SavaDev.Infrastructure.Util.ImageEditor.ImageEditor();
        }

        public void Dispose()
        {
            _imageEditor = null;
        }

        [Theory]
        [MemberData(nameof(ResizeData))]
        public void CountResize_Ok(int origWidth, int origHeight, ImageResizeOptions2 options, (int, int) expected)
        {
            // Arrange

            // Act
            var result = _imageEditor.CountResize(origWidth, origHeight, options);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(CropData))]
        public void CountCrop_Ok(int resWidth, int resHeight, ImageResizeOptions2 options, Rectangle expected)
        {
            // Arrange

            // Act
            var result = _imageEditor.CountCrop(resWidth, resHeight, options);

            // Assert
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> ResizeData => new List<object[]>
        {
            new object[] { 1000, 800, new ImageResizeOptions2(700, 233, true, false, ImageResizedOrientation.Horizontal), (700, 560) },
            new object[] { 1000, 800, new ImageResizeOptions2(700, 233, true, false, ImageResizedOrientation.AsOriginal), (700, 560) },
            new object[] { 800, 1000, new ImageResizeOptions2(700, 233, true, false, ImageResizedOrientation.Horizontal), (700, 875) },
            new object[] { 800, 1000, new ImageResizeOptions2(700, 233, true, false, ImageResizedOrientation.AsOriginal), (560, 700) },
            new object[] { 1000, 800, new ImageResizeOptions2(700, 233, true, false, ImageResizedOrientation.Vertical), (875, 700) },
            new object[] { 1000, 800, new ImageResizeOptions2(700, 700, true, false, ImageResizedOrientation.Square), (875, 700) },
        };

        public static IEnumerable<object[]> CropData => new List<object[]>
        {
            new object[] { 700, 560, new ImageResizeOptions2(700, 233, false, false, ImageResizedOrientation.Horizontal), new Rectangle(0, 0, 700, 233) },
            new object[] { 700, 560, new ImageResizeOptions2(700, 233, true, false, ImageResizedOrientation.Horizontal), new Rectangle(0, 163, 700, 233) },
            new object[] { 700, 560, new ImageResizeOptions2(700, 233, true, false, ImageResizedOrientation.AsOriginal), new Rectangle(0, 163, 700, 233) },
            new object[] { 700, 875, new ImageResizeOptions2(700, 233, true, false, ImageResizedOrientation.Horizontal), new Rectangle(0, 321, 700, 233) },
            new object[] { 560, 700, new ImageResizeOptions2(700, 233, true, false, ImageResizedOrientation.AsOriginal), new Rectangle(163, 0, 233, 700) },
        };
    }
}
