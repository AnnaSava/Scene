using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Base.Types.Fields;

namespace Framework.Base.Types.Registry
{
    public class FlexRegistryView<TModel, TFilterModel> : BaseRegistryView<TModel>
    {
        public Dictionary<string, FlexFieldViewSettings> FieldSettings { get; set; }
    }
}
