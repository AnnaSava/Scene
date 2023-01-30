using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Registry.Enums
{
    public enum MatchMode
    {
        Equals,
        NotEquals,
        In,
        NotIn,
        Lt,
        Lte,
        Gt,
        Gte,
        Between,
        StartsWith,
        EndsWith,
        Contains,
        NotContains,
        DateIs,
        DateIsNot,
        DateBefore,
        DateAfter
    }
}
