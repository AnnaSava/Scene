using System.Reflection;

namespace SavaDev.Infrastructure.Util.ModelFieldsManager
{
    public class ModelFieldsReader : BaseModelFieldsManager
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

        private readonly ModelFieldsReaderOptions _options;

        public ModelFieldsReader(ModelFieldsReaderOptions options)
        {
            _options = options;
        }

        public T ParseEntity<T>(Dictionary<string, string> input)
            where T : class, new()
        {
            var entity = new T();
            var eProps = GetEntityProps<T>();

            foreach (var parser in Parsers)
            {
                SetValues(parser.Key, entity, eProps, input);
            }

            return entity;
        }

        public void SetValues<T>(string typeName, T entity, IEnumerable<PropertyInfo> eProps, Dictionary<string, string> input)
        {
            foreach (var eProp in eProps)
            {
                object parsed = null;

                if (eProp.PropertyType.Name == typeName
                    || eProp.PropertyType.Name == NullableTypeNamePart && eProp.PropertyType.FullName.Contains(typeName))
                {
                    parsed = Parsers[typeName](input[eProp.Name]);
                }
                else if(eProp.PropertyType.FullName.Contains(EnumTypeNamePart))
                {
                    if(int.TryParse(input[eProp.Name], out int parsedInt))
                    {
                        parsed = parsedInt;
                    }
                }

                if (parsed != null)
                {
                    eProp.SetValue(entity, parsed);
                }
            }


            //var typeProps = eProps.Where(f => f.PropertyType.Name == typeName);

            //foreach (var prop in typeProps)
            //{
            //    var parsed = Parsers[prop.PropertyType.Name](input[prop.Name]);

            //    prop.SetValue(entity, parsed);
            //}

            //var nullableProps = eProps.Where(f => f.PropertyType.Name == NullableTypeNamePart && f.PropertyType.FullName.Contains(typeName));
            //foreach (var prop in nullableProps)
            //{
            //    if (!string.IsNullOrEmpty(input[prop.Name]))
            //    {
            //        foreach (var tpName in Parsers.Keys)
            //        {
            //            if (prop.PropertyType.FullName.Contains(tpName))
            //            {
            //                var parsed = Parsers[prop.PropertyType.Name](input[prop.Name]);

            //                prop.SetValue(entity, parsed);
            //                continue;
            //            }
            //        }
            //    }
            //}

            //var enumProps = eProps.Where(f => f.PropertyType.FullName.Contains(EnumTypeNamePart));
            //foreach (var prop in enumProps)
            //{
            //    if (int.TryParse(input[prop.Name], out int parsed))
            //    {
            //        prop.SetValue(entity, parsed);
            //    }
            //}
        }


    }
}
