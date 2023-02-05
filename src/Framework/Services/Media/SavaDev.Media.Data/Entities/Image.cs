using SavaDev.Base.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Media.Data.Entities
{
    public class Image : BaseRestorableEntity<Guid>
    {
        public Guid GalleryId { get; set; }

        public virtual Gallery Gallery { get; set; }

        public string PreviewId { get; set; }

        public string OwnerId { get; set; }

        public virtual ICollection<ImageFile> Files { get; set; }
    }
}
