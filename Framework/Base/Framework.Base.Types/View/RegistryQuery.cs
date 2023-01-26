using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Types.View
{
    public class RegistryQuery<TFilter>
    {
        public TFilter Filter { get; set; }

        public RegistryPageInfo PageInfo { get; set; }

        public IEnumerable<RegistrySort> Sort { get; set; }
    }
}
