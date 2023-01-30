using SavaDev.Base.Data.Registry.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Registry.Filter
{
    public class FilterField<T>
    {
        public T Value { get; set; }

        public MatchMode MatchMode { get; set; }
    }
}
