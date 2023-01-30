using Framework.Base.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Types.Registry
{
    public class NumericFilterField<T> where T : struct
    {
        public List<T> Value { get; set; }

        public MatchModeNumeric MatchMode { get; set; }
    }
}
