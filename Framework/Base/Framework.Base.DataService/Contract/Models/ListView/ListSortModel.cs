using Framework.Base.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Contract.Models
{
    [Obsolete]
    public class ListSortModel
    {
        public string FieldName { get; set; }

        public SortDirection Direction { get; set; }

        public bool Initial { get; set; } = true;
    }
}
