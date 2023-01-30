using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Contract.Models
{
    [Obsolete]
    public class PageInfoModel
    {
        public IEnumerable<ListSortModel> Sort { get; set; }

        private int pageNumber = 1;
        private int rowsCount = 10;

        public int PageNumber
        {
            get { return pageNumber; }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(PageNumber), $"Value must be greater than 0. Value: {value}");
                pageNumber = value;
            }
        }
        
        public int RowsCount
        {
            get { return rowsCount; }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(RowsCount), $"Value must be greater than 0. Value: {value}");
                rowsCount = value;
            }
        }

        public PageInfoModel() { }

        public PageInfoModel(int pageNumber, int rowsCount)
        {
            PageNumber = pageNumber;
            RowsCount = rowsCount;
        }
    }
}
