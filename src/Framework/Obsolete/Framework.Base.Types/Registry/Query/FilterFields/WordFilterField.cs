using Framework.Base.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Types.Registry
{
    [Obsolete]
    public class WordFilterField
    {
        public List<string> Value { get; set; }

        public MatchModeWord MatchMode { get; set; }
    }
}
