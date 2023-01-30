using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Types.Registry
{
    public abstract class BaseRegistryView<TModel>
    {
        public List<TModel> Items { get; set; }

        public int Page { get; set; }

        public int TotalPages { get; set; }

        public long TotalRows { get; set; }
    }
}
