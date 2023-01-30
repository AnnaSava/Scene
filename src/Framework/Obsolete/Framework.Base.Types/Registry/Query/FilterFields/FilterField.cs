using Framework.Base.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Types.Registry
{
    [Obsolete]
    public class FilterField<T>
    {
        public T Value { get; set; }

        public MatchMode MatchMode { get; set; }
    }
}
