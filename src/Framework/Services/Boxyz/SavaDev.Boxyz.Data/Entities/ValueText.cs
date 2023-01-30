using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Boxyz.Data.Entities
{
    public class ValueText : BaseValueEntity
    {
        public string CommonValue { get; set; }

        public virtual ICollection<ValueTextFolk> Folks { get; set; }
    }
}
