using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Media.Contract.Models
{
    public class ImageProcessorOptions
    {
        public IEnumerable<string> ImageResizeKinds { get; set; }

        private string previewImageKind;

        public string PreviewImageKind
        {
            get { return previewImageKind; }
            set
            {
                if (ImageResizeKinds != null && ImageResizeKinds.Contains(value))
                    previewImageKind = value;
                else
                    throw new ArgumentException($"Value {value} not found in {nameof(ImageResizeKinds)}");
            }
        }

        private string previewMobileImageKind;

        public string PreviewMobileImageKind
        {
            get { return previewMobileImageKind; }
            set
            {
                if (ImageResizeKinds != null && ImageResizeKinds.Contains(value))
                    previewMobileImageKind = value;
                else
                    throw new ArgumentException($"Value {value} not found in {nameof(ImageResizeKinds)}");
            }
        }
    }
}
