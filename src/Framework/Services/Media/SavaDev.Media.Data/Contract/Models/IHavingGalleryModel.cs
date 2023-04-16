using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Media.Data.Contract.Models
{
    public interface IHavingGalleryModel
    {
        public Guid? GalleryId { get; set; }
    }
}
