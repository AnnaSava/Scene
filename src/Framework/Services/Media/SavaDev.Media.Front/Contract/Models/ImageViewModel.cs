using SavaDev.Base.Front.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Media.Contract.Models
{
    public class ImageViewModel : BaseRegistryItemViewModel
    {
        public Guid Id { get; set; }

        public Guid GalleryId { get; set; }

        public string PreviewId { get; set; }

        public string Owner { get; set; }

        public string Module { get; set; }

        public virtual ICollection<ImageFileViewModel> Files { get; set; }
    }
}
