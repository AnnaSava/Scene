using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Front.Options
{
    public class ServiceOptions
    {
        public string ConnectionStringName { get; }

        public string TablePrefix { get; }

        public bool SilentResponse { get; set; }

        public ServiceOptions(string tablePrefix, string connectionStringName)
        {
            TablePrefix = tablePrefix;
            ConnectionStringName = connectionStringName;
        }
    }
}
