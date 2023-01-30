using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Boxyz.Data.Entities
{
    public class UniShape : BaseUniEntity<Shape>
    {
        public long UniBoardId { get; set; }

        public virtual UniBoard UniBoard { get; set; }

        public ICollection<UniBox> UniBoxes { get; set; }
    }
}
