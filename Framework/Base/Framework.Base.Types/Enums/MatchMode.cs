using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Types.Enums
{
    [Obsolete]
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

    [Obsolete]
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

    [Obsolete]
    public enum MatchModeWord
    {
        None,
        Equals,
        NotEquals,
        In,
        NotIn,
        StartsWith,
        EndsWith,
        Contains,
        NotContains,
    }

    [Obsolete]
    public enum MatchModeText
    {
        StartsWith,
        EndsWith,
        Contains,
        NotContains,
    }
}
