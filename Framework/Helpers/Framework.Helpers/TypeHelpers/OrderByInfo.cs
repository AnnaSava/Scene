using Framework.Base.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Helpers.TypeHelpers
{
    public class OrderByInfo
    {
        public string PropertyName { get; set; }
        public SortDirection Direction { get; set; }
        public bool Initial { get; set; }
    }
}
