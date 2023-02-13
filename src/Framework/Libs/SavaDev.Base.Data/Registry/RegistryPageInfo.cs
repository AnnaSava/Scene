using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Registry
{
    public class RegistryPageInfo
    {
        public int PageNumber { get; set; }

        public int RowsCount { get; set; }

        public RegistryPageInfo() { PageNumber = 1; RowsCount = 20; }

        public RegistryPageInfo(int pageNumber, int rowsCount)
        {
            PageNumber = pageNumber;
            RowsCount = rowsCount;
        }
    }
}
