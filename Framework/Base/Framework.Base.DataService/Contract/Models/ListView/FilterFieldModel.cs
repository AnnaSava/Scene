using Framework.Base.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Contract.Models
{
    public class FilterFieldModel<T>
    {
        public T Value { get; set; }

        public MatchMode MatchMode { get; set; }
    }

    public class WordFilterField
    {
        public List<string> Value { get; set; }

        public MatchModeWord MatchMode { get; set; }
    }

    public class TextFilterField
    {
        public string Value { get; set; }

        public MatchModeText MatchMode { get; set; }
    }

    public class NumericFilterField<T> where T : struct
    {
        public List<T> Value { get; set; }

        public MatchModeNumeric MatchMode { get; set; }
    }

}
