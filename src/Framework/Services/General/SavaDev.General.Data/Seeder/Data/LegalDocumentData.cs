using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.General.Data.Seeder.Data
{
    internal class LegalDocumentData
    {
        public static Dictionary<string, string> LegalDocumentRuCulture => new Dictionary<string, string>
        {
            { "terms", "Правила пользования сайтом" },
            { "privacy", "Защита информации" },
        };

        public static Dictionary<string, string> LegalDocumentEnCulture => new Dictionary<string, string>
        {
            { "terms", "Terms of use" },
            { "privacy", "Protection of information" },
        };
    }
}
