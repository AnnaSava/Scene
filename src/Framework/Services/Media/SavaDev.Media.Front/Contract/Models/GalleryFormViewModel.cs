using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Media.Contract.Models
{
    public class GalleryFormViewModel
    {
        public Guid Id { get; set; }

        public Guid? DraftId { get; set; }

        public string Owner { get; set; }

        public string AttachedToId { get; set; }

        public List<Guid> ImageIds { get; set; } = new List<Guid>();
    }
}
