using SavaDev.Boxyz.Data.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Boxyz.Data.Entities
{
    public class ShapeSideStamp : BaseEntity<Guid>
    {
        public string ConstName { get; set; }

        public Guid ShapeSideId { get; set; }

        public virtual ShapeSide ShapeSide { get; set; }

        public string Value { get; set; }

        public Shadow Shadow { get; set; }
    }
}
