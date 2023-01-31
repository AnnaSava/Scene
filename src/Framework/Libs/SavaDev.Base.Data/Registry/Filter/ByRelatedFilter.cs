using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Registry.Filter
{
    public class ByRelatedFilter<TKey>
    {
        public string FieldNameOfRelated { get; set; }

        public TKey RelatedId { get; set; }

        public bool WithDeleted { get; set; }

        public string SearchText { get; set; }

        public ByRelatedFilter(string fieldNameOfRelated, TKey relatedId, bool withDeleted = false, string searchText = null)
        {
            FieldNameOfRelated = fieldNameOfRelated;
            RelatedId = relatedId;
            WithDeleted = withDeleted;
            SearchText = searchText;
        }
    }
}
