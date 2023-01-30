using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Types.Image
{
    public class ImageResizeKinds
    {
        // TODO
        // mdpi(medium density) - resolution of 160x160 pixels
        // hdpi(high density) - resolution of 240x240 pixels
        // xhdpi(extra-high density) - resolution of 320x320 pixels
        // xxhdpi(extra-extra-high density) - resolution of 480x480 pixels
        // xxxhdpi(extra-extra-extra-high density) - resolution of 640x640 pixels

        private int Large = 1200;
        private int Middle = 700;
        private int Thumb = 200;
        private int ThumbMobile = 75;

        public ImageResizeKinds() { }

        public ImageResizeKinds(int large, int middle, int thumb, int thumbMobile)
        {
            Large = large;
            Middle = middle;
            Thumb = thumb;
            ThumbMobile = thumbMobile;
        }

        public Dictionary<string, (int, int, bool)> GetImageKinds() => new Dictionary<string, (int, int, bool)>
        {
            { ImageResizeKindName.Original, (0, 0, false) },
            { ImageResizeKindName.ResizedLarge, (Large, 0, false) },
            { ImageResizeKindName.ResizedMiddle, (Middle, 0, false) },
            { ImageResizeKindName.Thumb, (Thumb, 0, false) },
            { ImageResizeKindName.ThumbMobile, (ThumbMobile, 0, false) },
            { ImageResizeKindName.SquareLarge, (Large, 0, false) },
            { ImageResizeKindName.SquareMiddle, (Middle, 0, false) },
            { ImageResizeKindName.SquareThumb, (Thumb, 0, false) },
            { ImageResizeKindName.SquareThumbMobile, (ThumbMobile, 0, false) },
            { ImageResizeKindName.CenterSquareLarge, (Large, 0, true) },
            { ImageResizeKindName.CenterSquareMiddle, (Middle, 0, true) },
            { ImageResizeKindName.CenterSquareThumb, (Thumb, 0, true) },
            { ImageResizeKindName.CenterSquareThumbMobile, (ThumbMobile, 0, true) },
            { ImageResizeKindName.Large16x9, (Large, Get16x9smallSide(Large), false) },
            { ImageResizeKindName.Middle16x9, (Middle, Get16x9smallSide(Middle), false) },
            { ImageResizeKindName.Thumb16x9, (Thumb, Get16x9smallSide(Thumb), false) },
            { ImageResizeKindName.ThumbMobile16x9, (ThumbMobile, Get16x9smallSide(ThumbMobile), false) },
            { ImageResizeKindName.CenterLarge16x9, (Large, Get16x9smallSide(Large), true) },
            { ImageResizeKindName.CenterMiddle16x9, (Middle, Get16x9smallSide(Middle), true) },
            { ImageResizeKindName.CenterThumb16x9, (Thumb, Get16x9smallSide(Thumb), true) },
            { ImageResizeKindName.CenterThumbMobile16x9, (ThumbMobile, Get16x9smallSide(ThumbMobile), true) },
            { ImageResizeKindName.Large4x3, (Large, Get4x3smallSide(Large), false) },
            { ImageResizeKindName.Middle4x3, (Middle, Get4x3smallSide(Middle), false) },
            { ImageResizeKindName.Thumb4x3, (Thumb, Get4x3smallSide(Thumb), false) },
            { ImageResizeKindName.ThumbMobile4x3, (ThumbMobile, Get4x3smallSide(ThumbMobile), false) },
            { ImageResizeKindName.CenterLarge4x3, (Large, Get4x3smallSide(Large), true) },
            { ImageResizeKindName.CenterMiddle4x3, (Middle, Get4x3smallSide(Middle), true) },
            { ImageResizeKindName.CenterThumb4x3, (Thumb, Get4x3smallSide(Thumb), true) },
            { ImageResizeKindName.CenterThumbMobile4x3, (ThumbMobile, Get4x3smallSide(ThumbMobile), true) },
            { ImageResizeKindName.Large3x2, (Large, Get3x2smallSide(Large), false) },
            { ImageResizeKindName.Middle3x2, (Middle, Get3x2smallSide(Middle), false) },
            { ImageResizeKindName.Thumb3x2, (Thumb, Get3x2smallSide(Thumb), false) },
            { ImageResizeKindName.ThumbMobile3x2, (ThumbMobile, Get3x2smallSide(ThumbMobile), false) },
            { ImageResizeKindName.CenterLarge3x2, (Large, Get3x2smallSide(Large), true) },
            { ImageResizeKindName.CenterMiddle3x2, (Middle, Get3x2smallSide(Middle), true) },
            { ImageResizeKindName.CenterThumb3x2, (Thumb, Get3x2smallSide(Thumb), true) },
            { ImageResizeKindName.CenterThumbMobile3x2, (ThumbMobile, Get3x2smallSide(ThumbMobile), true) },
            { ImageResizeKindName.Large3x1, (Large, Get3x1smallSide(Large), false) },
            { ImageResizeKindName.Middle3x1, (Middle, Get3x1smallSide(Middle), false) },
            { ImageResizeKindName.Thumb3x1, (Thumb, Get3x1smallSide(Thumb), false) },
            { ImageResizeKindName.ThumbMobile3x1, (ThumbMobile, Get3x1smallSide(ThumbMobile), false) },
            { ImageResizeKindName.CenterLarge3x1, (Large, Get3x1smallSide(Large), true) },
            { ImageResizeKindName.CenterMiddle3x1, (Middle, Get3x1smallSide(Middle), true) },
            { ImageResizeKindName.CenterThumb3x1, (Thumb, Get3x1smallSide(Thumb), true) },
            { ImageResizeKindName.CenterThumbMobile3x1, (ThumbMobile, Get3x1smallSide(ThumbMobile), true) },
        };

        private int Get16x9smallSide(int side) => side * 9 / 16;
        private int Get3x2smallSide(int side) => side * 2 / 3;
        private int Get3x1smallSide(int side) => side / 3;
        private int Get4x3smallSide(int side) => side * 3 / 4;
    }
}
