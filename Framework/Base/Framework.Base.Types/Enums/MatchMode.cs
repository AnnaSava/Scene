using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Types.Enums
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

    public enum MatchModeNumeric
    {
        Equals,
        NotEquals,
        In,
        NotIn,
        Lt,
        Lte,
        Gt,
        Gte,
        Between
    }

    public enum MatchModeWord
    {
        Equals,
        NotEquals,
        In,
        NotIn,
        StartsWith,
        EndsWith,
        Contains,
        NotContains,
    }

    public enum MatchModeText
    {
        StartsWith,
        EndsWith,
        Contains,
        NotContains,
    }
}
