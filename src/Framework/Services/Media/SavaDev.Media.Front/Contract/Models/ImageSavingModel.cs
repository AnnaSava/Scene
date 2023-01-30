using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Media.Contract.Models
{
    public class ImageSavingModel
    {
        public byte[] Content { get; set; }

        public Guid? GalleryId { get; set; }

        public string Module { get; set; }

        public string OwnerId { get; set; }
    }
}
