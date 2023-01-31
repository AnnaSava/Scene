using SavaDev.Base.Data.Registry.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Registry
{
    public class RegistrySort
    {
        public string FieldName { get; set; }

        public SortDirection Direction { get; set; }

        public RegistrySort(string fieldName, SortDirection direction = SortDirection.Asc)
        {
            FieldName = fieldName;
            Direction = direction;
        }
    }
}
