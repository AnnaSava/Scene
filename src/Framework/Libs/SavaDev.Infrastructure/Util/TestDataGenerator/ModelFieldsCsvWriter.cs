using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Util.TestDataGenerator
{
    public class ModelFieldsCsvWriter : BaseModelFieldsManager
    {
        private readonly ModelFieldsCsvWriterOptions _options;

        public ModelFieldsCsvWriter(ModelFieldsCsvWriterOptions options, ITestDataProvider dataProvider) : base(dataProvider)
        {
            _options = options;
        }

        public async Task WriteTestDataToFile<T>()
            where T : class, new()
        {
            string? resultString;
            if (_options.UseMethodNameColumn)
            {
                var data = _dataGenerator.ProvideTestDataWithMethodColumn<T>(1000);
                resultString = MakeTestDataWithMethodCsvString(data);
            }
            else
            {
                var data = _dataGenerator.ProvideTestData<T>(1000);
                resultString = MakeTestDataCsvString(data);
            }
            await WriteFile<T>(resultString);
        }

        private async Task WriteFile<T>(string content)
        {
            if (!Directory.Exists(_options.OutputFolderName))
            {
                Directory.CreateDirectory(_options.OutputFolderName);
            }

            var filePath = $"{_options.OutputFolderName}\\{typeof(T).Name.ToLower()}_{DateTime.Now.Ticks}.csv";

            using (var sw = new StreamWriter(filePath))
            {
                await sw.WriteAsync(content);
            }
        }

        private string MakeTestDataCsvString<T>(IEnumerable<T> data)
        {
            var sb = new StringBuilder();

            var propNames = GetModelPropNamesOrdered<T>().ToList();
            sb.AppendLine(MakeJoinedHeaderString(propNames));

            foreach (var model in data)
            {
                var filling = MakeJoinedValuesString(propNames, model);

                sb.AppendLine(filling);
            }
            return sb.ToString();
        }

        private string MakeTestDataWithMethodCsvString<T>(IEnumerable<KeyValuePair<string, T>> data)
        {
            var sb = new StringBuilder();

            sb.Append(MethodColumnHeader + _options.CsvSeparator);

            var propNames = GetModelPropNamesOrdered<T>().ToList();
            sb.AppendLine(MakeJoinedHeaderString(propNames));

            foreach (var model in data)
            {
                sb.Append(model.Key == null ? "" : model.Key);
                sb.Append(_options.CsvSeparator);

                var filling = MakeJoinedValuesString(propNames, model.Value);
                sb.AppendLine(filling);
            }
            return sb.ToString();
        }

        private string MakeJoinedHeaderString(IList<string> orderedPropNames)
        {
            var header = string.Join(_options.CsvSeparator, orderedPropNames);
            return header;
        }

        private string MakeJoinedValuesString<T>(IList<string> orderedPropNames, T model)
        {
            var values = new object[orderedPropNames.Count()];

            for (var i = 0; i < orderedPropNames.Count(); i++)
            {
                var prop = orderedPropNames[i];
                var val = typeof(T)?.GetProperty(prop)?.GetValue(model);
                if (val != null)
                {
                    values[i] = val;
                }
            }

            var filling = string.Join(_options.CsvSeparator, values.Select(m => m == null ? "" : m.ToString()));

            return filling;
        }
    }
}
