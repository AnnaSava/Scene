using SavaDev.Base.Data.Registry.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Registry.Filter
{
    public class WordFilterField
    {
        public List<string> Value { get; set; }

        public MatchModeWord MatchMode { get; set; }
    }
}
