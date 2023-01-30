using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Service.ListView
{
    [Obsolete]
    public class ListPageInfoViewModel
    {
        public string Sort { get; set; }

        public int Page { get; set; } = 1;

        public int Rows { get; set; } = 50;
    }
}
