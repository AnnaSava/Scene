using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Media.Data.Entities
{
    public class ImageFile
    {
        public Guid ImageId { get; set; }

        public virtual Image Image { get; set; }

        public string FileId { get; set; }

        public string Kind { get; set; }
    }
}
