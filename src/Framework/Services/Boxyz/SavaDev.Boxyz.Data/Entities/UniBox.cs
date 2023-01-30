using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Boxyz.Data.Entities
{
    public class UniBox : BaseUniEntity<Box>
    {
        public long UniShapeId { get; set; }

        public virtual UniShape UniShape { get; set; }

        public virtual ICollection<Box> Editions { get; set; }
    }
}
