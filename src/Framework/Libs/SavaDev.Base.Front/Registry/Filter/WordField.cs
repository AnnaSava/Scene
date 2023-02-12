using SavaDev.Base.Data.Registry.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Front.Registry.Filter
{
    public class WordField
    {
        public MatchModeWord Match { get; set; }

        public string Text { get; set; }

        public string ToSearchString()
        {
            return Match.ToString().ToLower() + "|" + Text;
        }
    }
}
