using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Registry.Filter
{
    public class ByIdsFilter<TKey> : BaseFilter
    {
        public ICollection<TKey> Ids { get; set; } = new List<TKey>();

        public bool WithDeleted { get; set; }

        public string SearchText { get; set; }

        public ByIdsFilter(ICollection<TKey> ids, bool withDeleted = false, string searchText = null)
        {
            Ids = ids;
            WithDeleted = withDeleted;
            SearchText = searchText;
        }
    }
}
