using SavaDev.Base.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Media.Data.Entities
{
    public class Gallery : BaseHubEntity
    {
        public virtual ICollection<Image> Images { get; set; }
    }
}
