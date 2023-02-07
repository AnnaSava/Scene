using SavaDev.Base.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Media.Data.Contract.Models
{
    public class ImageModel : IFormModel, IAnyModel
    {
        public Guid Id { get; set; }

        public Guid GalleryId { get; set; }

        public string PreviewId { get; set; }

        public string OwnerId { get; set; }

        public virtual ICollection<ImageFileModel> Files { get; set; }
    }
}
