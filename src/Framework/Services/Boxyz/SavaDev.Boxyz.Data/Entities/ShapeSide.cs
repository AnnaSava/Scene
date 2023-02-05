using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Boxyz.Data.Entities
{
    public class ShapeSide : BaseEntity<Guid>
    {
        public string ConstName { get; set; }

        public Guid ShapeId { get; set; }

        public virtual Shape Shape { get; set; }

        public string DataType { get; set; }

        public int OrderNumber { get; set; }

        public virtual ICollection<ShapeSideFolk> Folks { get; set; }

        public virtual ICollection<ShapeSideStamp> Stamps { get; set; }
    }
}
