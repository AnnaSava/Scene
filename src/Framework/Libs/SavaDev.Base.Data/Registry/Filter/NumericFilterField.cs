using SavaDev.Base.Data.Registry.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Registry.Filter
{
    public class NumericFilterField<T> where T : struct
    {
        public List<T> Value { get; set; }

        public MatchModeNumeric MatchMode { get; set; }
    }
}
