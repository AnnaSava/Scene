using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Boxyz.Data.Entities
{
    public class ValueLink : BaseValueEntity
    {
        public Guid LinkedBoxId { get; set; }

        public virtual Box LinkedBox { get; set; }
    }
}
