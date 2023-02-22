using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Util.ImageEditor
{
    public class ImageEditor
    {
        public void SquareCrop(byte[] content, Stream outStream)
        {
            var smallFile = Image.Load(content);

            var side = smallFile.Width > smallFile.Height ? smallFile.Height : smallFile.Width;

            var top_bottom = (smallFile.Height - side) / 2;
            var left_right = (smallFile.Width - side) / 2;

            //smallFile.Crop(top_bottom, left_right, top_bottom, left_right)
            //    .Resize(201, 201, true, true)
            //    .Crop(1, 1);

            //return smallFile.GetBytes();

            var rect = new Rectangle(left_right, top_bottom, side, side);

            var cropped = smallFile.Clone(ctx => ctx.Crop(rect));

            // TODO учитывать формат картинки
            //cropped.Save(outStream, new PngEncoder());
            cropped.Save(outStream, new JpegEncoder());
        }

        public void Resize(byte[] content, Stream outStream)
        {
            var smallFile = Image.Load(content);

            var side = smallFile.Width > smallFile.Height ? smallFile.Height : smallFile.Width;

            var top_bottom = (smallFile.Height - side) / 2;
            var left_right = (smallFile.Width - side) / 2;

            //smallFile.Crop(top_bottom, left_right, top_bottom, left_right)
            //    .Resize(201, 201, true, true)
            //    .Crop(1, 1);

            //return smallFile.GetBytes();

            var resized = smallFile.Clone(ctx => ctx.Resize(smallFile.Width / 2, smallFile.Height / 2));

            // TODO учитывать формат картинки
            resized.Save(outStream, new JpegEncoder());
        }

        public void CropResize(byte[] content, Stream outStream, int largeSide, int smallSide, bool centerCrop)
        {
            var imageFile = Image.Load(content);
            if (largeSide == 0)
            {
                imageFile.Save(outStream, new JpegEncoder());
                return;
            }

            var origLargeSide = Math.Max(imageFile.Width, imageFile.Height);
            var origSmallSide = Math.Min(imageFile.Width, imageFile.Height);

            var isVertical = imageFile.Height > imageFile.Width;

            var resizedSmallSide = origSmallSide * largeSide / origLargeSide;

            var needCrop = resizedSmallSide > smallSide;

            var resized = imageFile.Clone(ctx => ctx.Resize(isVertical ? largeSide : resizedSmallSide, isVertical ? resizedSmallSide : largeSide));

            var t = resized;

            if (needCrop)
            {
                var top_bottom = centerCrop ? (resized.Height - (resized.Height > resized.Width ? largeSide : smallSide)) / 2 : 0;
                var left_right = centerCrop ? (resized.Width - (resized.Width >= resized.Height ? largeSide : smallSide)) / 2 : 0;

                var rect = new Rectangle(left_right, top_bottom, isVertical ? smallSide : largeSide, isVertical ? largeSide : smallSide);

                var cropped = resized.Clone(ctx => ctx.Crop(rect));
                // TODO учитывать формат картинки
                //cropped.Save(outStream, new PngEncoder());
                cropped.Save(outStream, new JpegEncoder());
            }
            else
            {
                // TODO учитывать формат картинки
                resized.Save(outStream, new JpegEncoder());
            }
        }

        public (int, int) GetSize(byte[] content)
        {
            var img = Image.Load(content);
            return (img.Width, img.Height);
        }
    }
}
