using Framework.Base.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Contract
{
    public class ModelBuilderConfigurationOptions 
    {
        [Obsolete]
        public string TablePrefix { get; set; }

        [Obsolete]
        public NamingConvention NamingConvention { get; set; }

        public ModelBuilderConfigurationOptions()
        {
        }
    }
}
