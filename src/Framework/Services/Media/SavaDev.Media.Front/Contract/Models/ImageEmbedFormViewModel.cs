using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Media.Contract.Models
{
    public class ImageEmbedFormViewModel
    {
        public Guid Id { get; set; }

        public Guid GalleryId { get; set; }

        public string PreviewId { get; set; }

        public string PreviewPath { get; set; }
    }
}
