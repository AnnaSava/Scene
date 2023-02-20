using Framework.Base.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Contract.Models
{
    [Obsolete]
    public class WordFilterField
    {
        public List<string> Value { get; set; }

        public MatchModeWord MatchMode { get; set; }
    }

    [Obsolete]
    public class NumericFilterField<T> where T : struct
    {
        public List<T> Value { get; set; }

        public MatchModeNumeric MatchMode { get; set; }
    }

}
