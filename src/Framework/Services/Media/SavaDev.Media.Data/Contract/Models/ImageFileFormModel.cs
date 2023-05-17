using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Media.Data.Contract.Models
{
    public class ImageFileFormModel
    {
        public Guid ImageId { get; set; }

        public string FileId { get; set; }

        public string Kind { get; set; } // Web, FullSize, Thumb etc.
    }
}
