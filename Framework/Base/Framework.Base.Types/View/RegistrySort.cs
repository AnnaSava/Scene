using Framework.Base.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Types.View
{
    public class RegistrySort
    {
        public string FieldName { get; set; }

        public SortDirection Direction { get; set; }

        public bool Initial { get; set; } = true;
    }
}
