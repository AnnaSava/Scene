using Framework.Base.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Types.DataStorage
{
    public class TableNameHelper
    {
        private string tablePrefix;
        private Dictionary<string, string> tables;
        private NamingConvention namingConvention;

        public TableNameHelper(Dictionary<string, string> tables, NamingConvention namingConvention, string tablePrefix)
        {
            this.tables = tables;
            this.namingConvention = namingConvention;
            this.tablePrefix = tablePrefix;
        }

        public string GetTableName(string entityName)
        {
            if (tables == null) return null;

            if (namingConvention == NamingConvention.SnakeCase)
                return tablePrefix + tables[entityName];

            return tablePrefix + entityName + "s";
        }
    }
}
