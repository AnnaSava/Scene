using SavaDev.Base.Front.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Media.Contract.Models
{
    public class GalleryViewModel : BaseRegistryItemViewModel
    {
        public Guid Id { get; set; }

        public string Owner { get; set; }

        public string AttachedToId { get; set; }
    }
}
