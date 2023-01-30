using Framework.Base.Types.Enums;
using Framework.Helpers.TypeHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Helpers
{
    public class TableNameHelper
    {
        private string tablePrefix;
        private NamingConvention namingConvention;

        public TableNameHelper(NamingConvention namingConvention, string tablePrefix)
        {
            this.namingConvention = namingConvention;
            this.tablePrefix = tablePrefix;
        }

        public string GetTableName(string entityName)
        {
            if (entityName == null) return null;

            if (namingConvention == NamingConvention.SnakeCase)
                return GetMultipleWord(tablePrefix + entityName.ToSnakeCase());

            return GetMultipleWord(tablePrefix + entityName);
        }

        private string GetMultipleWord(string word)
        {
            if(word.EndsWith('y'))
            {
                word = word.Substring(0, word.Length - 1) + "ies";
                return word;
            }

            return word + "s";
        }
    }
}
