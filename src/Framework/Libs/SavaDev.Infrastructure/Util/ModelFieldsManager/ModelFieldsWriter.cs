using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Util.ModelFieldsManager
{
    public class ModelFieldsWriter : BaseModelFieldsManager
    {
        readonly Dictionary<string, Func<object>> Defaults = new Dictionary<string, Func<object>>
        {
            { nameof(String), () => "any test text " },
            { nameof(Boolean), () => false },
            { nameof(Int32), () => 0 },
            { nameof(Int64), () => 0 },
            { nameof(Guid), () => Guid.NewGuid() },
            { nameof(DateTime), () => DateTime.Now },
        };

        private readonly IEnumerable<string> Types;

        private readonly ModelFieldsWriterOptions _options;

        public ModelFieldsWriter(ModelFieldsWriterOptions options)
        {
            _options = options;
            Types = Defaults.Select(m => m.Key);
        }

        public async Task MakeCsvTemplate<T>()
        {
            var eProps = GetEntityProps<T>();
            var data = GenerateTestData(eProps, _options.RowsCountToGenerate);
            var resultString = MakeTestDataCsvString(eProps, data);

            if (!Directory.Exists(_options.OutputFolderName))
            {
                Directory.CreateDirectory(_options.OutputFolderName);
            }

            var filePath = $"{_options.OutputFolderName}\\{typeof(T).Name.ToLower()}_{DateTime.Now.Ticks}.csv";

            using (var sw = new StreamWriter(filePath))
            {
                await sw.WriteAsync(resultString);
            }
        }

        public string MakeTestDataCsvString(IEnumerable<PropertyInfo> eProps, IEnumerable<Dictionary<string, object>> data)
        {
            var sb = new StringBuilder();

            if (_options.InsertTestNameColumn)
            {
                sb.Append(MethodColumnHeader + _options.CsvSeparator);
            }
            var header = string.Join(_options.CsvSeparator, eProps.OrderBy(m => m.Name).Select(f => f.Name));
            sb.AppendLine(header);

            foreach (var dict in data)
            {
                if (_options.InsertTestNameColumn)
                {
                    sb.Append(_options.CsvSeparator);
                }
                var filling = string.Join(_options.CsvSeparator, dict.OrderBy(m => m.Key).Select(m => m.Value == null ? "" : m.Value.ToString()));
                sb.AppendLine(filling);
            }
            return sb.ToString();
        }

        public IEnumerable<Dictionary<string, object>> GenerateTestData(IEnumerable<PropertyInfo> eProps, int rowsCount)
        {
            var list = new List<Dictionary<string, object>>();


            for (var i = 0; i < rowsCount; i++)
            {
                var dict = new Dictionary<string, object>();

                foreach (var prop in eProps)
                {
                    dict.Add(prop.Name, null);
                    var idNumber = i + 1;

                    if (prop.Name == IdPropertyName)
                    {
                        dict[prop.Name] = idNumber;
                    }
                    else
                    {
                        FillDataByTypes(dict, prop, idNumber);
                    }
                }
                list.Add(dict);
            }
            return list;
        }

        public void FillDataByTypes(Dictionary<string, object> dict, PropertyInfo prop, int idNumber)
        {
            var random = new Random();
            foreach (var typeName in Types)
            {
                if (prop.PropertyType.FullName.Contains(typeName))
                {
                    if (typeName == nameof(String) && prop.Name.EndsWith(IdPropertyName))
                    {
                        dict[prop.Name] = idNumber.ToString();
                        continue;
                    }

                    dict[prop.Name] = Defaults[typeName]();

                    if (typeName == nameof(String))
                    {
                        dict[prop.Name] += $"{idNumber} {random.Next(idNumber, idNumber + 5)}";
                    }
                    continue;
                }
            }
            if (prop.PropertyType.FullName.Contains(EnumTypeNamePart))
            {
                dict[prop.Name] = 0;
            }
        }
    }
}
