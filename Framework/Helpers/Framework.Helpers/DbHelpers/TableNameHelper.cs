using Framework.Base.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Helpers
{
    public class TableNameHelper
    {
        private Dictionary<string, string> tables;
        private NamingConvention namingConvention;

        public TableNameHelper(Dictionary<string, string> tables, NamingConvention namingConvention)
        {
            this.tables = tables;
            this.namingConvention = namingConvention;
        }

        // Key содержит nameof(EntityType), Value - название таблицы в snake case
        // Это костыль, чтобы использовать разные конвенции наименования в кастомных настройках имен таблиц
        // Нужен для настройки префиксов, а также теоретически для работы с разными провайдерами бд
        // (напр. SQL Server и Npg - они юзают разные name conventions)
        // TODO В дальнейшем написать нормальную реализацию
        public string GetTableName(string entityName)
        {
            if (tables == null) return null;

            if (namingConvention == NamingConvention.SnakeCase)
                return tables[entityName];

            return entityName + "s";
        }
    }
}
