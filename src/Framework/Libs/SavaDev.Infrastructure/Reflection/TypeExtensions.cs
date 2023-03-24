using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Reflection
{
    public static class TypeExtensions
    {
        public static List<T> GetAllPublicConstantValues<T>(this Type type)
        {
            var vals = new List<T>();

            var types = type.GetNestedTypes(BindingFlags.Public | BindingFlags.Static);
            foreach (var t in types)
            {
                var fields = t.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                var filtered = fields.Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T));
                vals.AddRange(filtered.Select(x => (T)x.GetRawConstantValue()).ToList());
            }
            return vals;
        }
    }
}
