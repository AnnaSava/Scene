using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.UiBlazorStrap.Registry.View
{
    public static class ValueHelper
    {
        public static string GetValue<T>(string columnName, T item)
        {
            var prop = typeof(T).GetProperty(columnName);
            if (prop == null) return "`Invalid property name`";
            return prop.GetValue(item)?.ToString() ?? "`NULL`";
        }
    }
}
