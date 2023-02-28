using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Types.Enums
{
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
}
