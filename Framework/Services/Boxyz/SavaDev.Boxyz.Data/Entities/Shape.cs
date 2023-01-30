using SavaDev.Boxyz.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Boxyz.Data.Entities
{
    public class Shape : BaseEditionEntity<UniShape>, IEdition
    {
        public Guid BoardId { get; set; }

        public virtual Board Board { get; set; }

        public virtual ICollection<ShapeSide> Sides { get; set; }

        public virtual ICollection<ShapeFolk> Folks { get; set; }

        public virtual ICollection<Box> Boxes { get; set; }
    }
}
