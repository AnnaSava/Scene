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
            { nameof(String), p => p },
            { nameof(Boolean), p => { bool.TryParse(p, out bool res); return res; } },
            { nameof(Int32), p => { int.TryParse(p, out int res); return res; } },
            { nameof(Int64), p => { long.TryParse(p, out long res); return res; } },
            { nameof(Guid), p => { Guid.TryParse(p, out Guid res); return res; } },
            { nameof(DateTime), p => { DateTime.TryParse(p, out DateTime res); return res; } },
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

        public KeyValuePair<string, T> ParseModel<T>(IEnumerable<PropertyInfo> modelProps, IEnumerable<object> input, bool useMethodColumn)
            where T : class, new()
        {
            var model = new T();
            var listInput = useMethodColumn ? input.Skip(1).ToList() : input.ToList();
            var listePropps = modelProps.OrderBy(m => m.Name).ToList();

            foreach (var parser in Parsers)
            {
                SetValues(parser.Key, model, listePropps, listInput);
            }

            var methodName = useMethodColumn ? input.First().ToString() : string.Empty;

            return new KeyValuePair<string, T>(methodName, model);
        }

        public void SetValues<T>(string typeName, T entity, IEnumerable<PropertyInfo> modelProps, Dictionary<string, string> input)
        {
            foreach (var eProp in modelProps)
            {
                object parsed = null;

                if (eProp.PropertyType.Name == typeName
                    || eProp.PropertyType.Name == Constants.NullableTypeNamePart && eProp.PropertyType.FullName.Contains(typeName))
                {
                    parsed = Parsers[typeName](input[eProp.Name]);
                }
                else if (eProp.PropertyType.FullName.Contains(Constants.EnumTypeNamePart))
                {
                    if (int.TryParse(input[eProp.Name], out int parsedInt))
                    {
                        parsed = parsedInt;
                    }
                }

                if (parsed != null)
                {
                    eProp.SetValue(entity, parsed);
                }
            }
        }

        public void SetValues<T>(string typeName, T entity, List<PropertyInfo> modelProps, List<object>? input)
        {
            for (var i = 0; i< modelProps.Count(); i++)
            {
                var eProp = modelProps[i];

                object parsed = null;

                if (eProp.PropertyType.Name == typeName
                    || eProp.PropertyType.Name == Constants.NullableTypeNamePart && eProp.PropertyType.FullName.Contains(typeName))
                {
                    parsed = Parsers[typeName](input[i].ToString());
                }
                else if (eProp.PropertyType.FullName.Contains(Constants.EnumTypeNamePart))
                {
                    if (int.TryParse(input[i].ToString(), out int parsedInt))
                    {
                        parsed = parsedInt;
                    }
                }

                if (parsed != null)
                {
                    eProp.SetValue(entity, parsed);
                }
            }
        }
    }
}
