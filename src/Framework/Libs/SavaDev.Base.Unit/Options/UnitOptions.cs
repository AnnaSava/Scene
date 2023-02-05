using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Unit.Options
{
    public class UnitOptions
    {
        public string ConnectionStringName { get; }

        public string TablePrefix { get; }

        public UnitOptions(string tablePrefix, string connectionStringName)
        {
            TablePrefix = tablePrefix;
            ConnectionStringName = connectionStringName;
        }
    }
}
