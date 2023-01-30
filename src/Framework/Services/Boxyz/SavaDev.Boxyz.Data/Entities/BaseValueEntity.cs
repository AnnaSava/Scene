using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Boxyz.Data.Entities
{
    public abstract class BaseValueEntity : BaseEntity<Guid>
    {
        public Guid BoxSideId { get; set; }

        public virtual BoxSide BoxSide { get; set; }

        public int OrderNumber { get; set; }

        public bool IsDeleted { get; set; }
    }
}
