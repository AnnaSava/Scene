using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Util.TestDataGenerator
{
    public class FieldsTestDataParser : ITestDataParser
    {
        readonly Dictionary<string, Func<string, object>> Parsers = new Dictionary<string, Func<string, object>>
        {          
            { nameof(Boolean), p => { bool.TryParse(p, out bool res); return res; } },
            { nameof(Int32), p => { int.TryParse(p, out int res); return res; } },
            { nameof(Int64), p => { long.TryParse(p, out long res); return res; } },
            { nameof(Guid), p => { Guid.TryParse(p, out Guid res); return res; } },
            { nameof(DateTime), p => { DateTime.TryParse(p, out DateTime res); return res; } },
            { nameof(String), p => p },
        };


        public T ParseModel<T>(IEnumerable<PropertyInfo> modelProps, Dictionary<string, string> input)
            where T : class, new()
        {
            var model = new T();
            foreach (var parser in Parsers)
            {
                SetValues(parser.Key, model, modelProps, input);
            }

            return model;
        }

        public T ParseModel<T>(IEnumerable<PropertyInfo> modelProps, Dictionary<string, object> input)
            where T : class, new()
        {
            var model = new T();
            foreach (var parser in Parsers)
            {
                SetValues(parser.Key, model, modelProps, input.ToDictionary(m => m.Key, v => v.Value?.ToString()));
            }

            return model;
        }

        public void SetValues<T>(string typeName, T model, IEnumerable<PropertyInfo> modelProps, Dictionary<string, string> input)
        {
            foreach (var modelProp in modelProps)
            {
                object parsed = null;

                if (modelProp.PropertyType.Name == typeName
                    || modelProp.PropertyType.Name == Constants.NullableTypeNamePart && modelProp.PropertyType.FullName.Contains(typeName))
                {
                    parsed = Parsers[typeName](input[modelProp.Name]);
                }
                else if (modelProp.PropertyType.FullName.Contains(Constants.EnumTypeNamePart))
                {
                    if (int.TryParse(input[modelProp.Name], out int parsedInt))
                    {
                        parsed = parsedInt;
                    }
                }

                if (parsed != null)
                {
                    try
                    {
                        modelProp.SetValue(model, parsed);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }
}
