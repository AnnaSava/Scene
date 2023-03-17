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

            var needCrop = false;// resizedSmallSide > smallSide;

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

        public void CropResize(byte[] content, Stream outStream, ImageResizeOptions2 options)
        {
            var imageFile = Image.Load(content);
            if (options.LargeSide == 0)
            {
                imageFile.Save(outStream, new JpegEncoder());
                return;
            }

            var sides = CountResize(imageFile.Width, imageFile.Height, options);
            var resized = imageFile.Clone(ctx => ctx.Resize(sides.Item1, sides.Item2));
            var rect = CountCrop(resized.Width, resized.Height, options);

            var cropped = resized.Clone(ctx => ctx.Crop(rect));

            // TODO учитывать формат картинки
            cropped.Save(outStream, new JpegEncoder());
        }

        public (int, int) CountResize(int origWidth, int origHeight, ImageResizeOptions2 options)
        {
            var result = (0, 0);

            if (options.Orientation == ImageResizedOrientation.AsOriginal)
            {
                var origLargeSide = Math.Max(origWidth, origHeight);
                var origSmallSide = Math.Min(origWidth, origHeight);
                var origIsVertical = origHeight > origWidth;

                var resizedSmallSide = origSmallSide * options.LargeSide / origLargeSide;
                result = (origIsVertical ? resizedSmallSide : options.LargeSide, origIsVertical ? options.LargeSide : resizedSmallSide);
            }
            else if (options.Orientation == ImageResizedOrientation.Horizontal)
            {
                var resized2ndSide = options.LargeSide * origHeight / origWidth;
                result = (options.LargeSide, resized2ndSide);
            }
            else if (options.Orientation == ImageResizedOrientation.Vertical)
            {
                var resized2ndSide = options.LargeSide * origWidth / origHeight;
                result = (resized2ndSide, options.LargeSide);
            }
            else if (options.Orientation == ImageResizedOrientation.Square)
            {
                var origLargeSide = Math.Max(origWidth, origHeight);
                var origSmallSide = Math.Min(origWidth, origHeight);
                var origIsVertical = origHeight > origWidth;
                var resized2ndSide = origIsVertical ? options.LargeSide * origSmallSide / origLargeSide : options.LargeSide * origLargeSide / origSmallSide;

                result = (origIsVertical ? options.LargeSide : resized2ndSide, origIsVertical ? resized2ndSide : options.LargeSide);
            }

            return result;
        }

        public Rectangle CountCrop(int resWidth, int resHeight, ImageResizeOptions2 options)
        {
            int top_bottom = 0, left_right = 0;
            var rect = new Rectangle();

            if (options.Orientation == ImageResizedOrientation.AsOriginal)
            {
                var origIsVertical = resHeight > resWidth;

                var diff = Math.Abs(origIsVertical ? resWidth - options.SmallSide : resHeight - options.SmallSide);
                if (options.CenterCrop)
                {
                    diff = diff / 2;
                    if (origIsVertical)
                    {
                        left_right += diff;
                    }
                    else
                    {
                        top_bottom += diff;
                    }
                }

                rect = new Rectangle(left_right, top_bottom, origIsVertical ? options.SmallSide : options.LargeSide, origIsVertical ? options.LargeSide : options.SmallSide);
            }
            else if (options.Orientation == ImageResizedOrientation.Horizontal)
            {
                if (options.CenterCrop)
                {
                    top_bottom = (resHeight - options.SmallSide) / 2;
                }

                rect = new Rectangle(left_right, top_bottom, options.LargeSide, options.SmallSide);
            }
            return rect;
        }

        public (int, int) GetSize(byte[] content)
        {
            var img = Image.Load(content);
            return (img.Width, img.Height);
        }
    }
}
