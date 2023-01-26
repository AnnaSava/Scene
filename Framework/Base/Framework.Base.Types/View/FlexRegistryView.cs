using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Types.View
{
    public class FlexRegistryView<TModel, TFilterModel> : BaseRegistryView<TModel>
    {
        public Dictionary<string, FlexFieldViewSettings> FieldSettings { get; set; }
    }
}
