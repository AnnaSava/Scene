using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Contract.Models
{
    public interface IFilter { }

    public interface IFilterIsDeleted
    {
        bool IsDeleted { get; set; }
    }

    public class ListFilterModel : IFilter, IFilterIsDeleted
    {
        public NumericFilterField<long> Ids { get; set; }

        public string SearchText { get; set; }

        public bool StartsWith { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class ListFilterModel<TKey> : IFilter, IFilterIsDeleted
        where TKey : struct
    {
        public NumericFilterField<TKey> Ids { get; set; }

        public string SearchText { get; set; }

        public bool StartsWith { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class SimpleFilterModel : IFilter
    {
        public WordFilterField SearchText { get; set; }
    }
}
