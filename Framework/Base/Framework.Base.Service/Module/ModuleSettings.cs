using Framework.Base.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Service.Module
{
   public class ModuleSettings
    { 
        public string ConnectionStringName { get;  }

        public string TablePrefix { get;  }

        public ModuleSettings(string tablePrefix, string connectionStringName)
        {
            TablePrefix = tablePrefix;
            ConnectionStringName = connectionStringName;
        }
    }
}
