using SavaDev.Boxyz.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Boxyz.Data.Entities
{
    public class Box : BaseEditionEntity<UniBox>, IEdition
    {
        public long UniqId { get; set; }

        public virtual UniBox Uniq { get; set; }

        public Guid ShapeId { get; set; }

        public virtual Shape Shape { get; set; }

        public virtual ICollection<BoxSide> Sides { get; set; }

        public virtual ICollection<ValueLink> LinkedBoxes { get; set; }
    }
}
