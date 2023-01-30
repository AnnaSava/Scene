using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Media.Contract.Models
{
    public class ImageFileViewModel
    {
        public Guid ImageId { get; set; }

        public string FileId { get; set; }

        public string Kind { get; set; }
    }
}
