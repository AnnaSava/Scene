using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Util.ModelFieldsManager
{
    public class ModelFieldsSqlReader : BaseModelFieldsManager, IModelFieldsReader
    {
        private readonly ModelFieldsSqlReaderOptions _options;

        public ModelFieldsSqlReader(ModelFieldsSqlReaderOptions options, ITestDataParser dataParser) : base(dataParser)
        {
            _options = options;
        }

        public List<T> GetModels<T>()
            where T : class, new()
        {
            var data = GetModels<T>(useMethodColumn: false);
            var modelProps = GetModelProps<T>().ToList();

            var result = new List<T>();

            foreach (var item in data)
            {
                var parsed = _dataParser.ParseModel<T>(modelProps, item);
                result.Add(parsed);
            }
            return result;
        }

        public List<KeyValuePair<string, T>> GetModelsWithTestMethods<T>()
            where T : class, new()
        {
            var data = GetModels<T>(useMethodColumn: true);
            var modelProps = GetModelProps<T>().ToList();

            var result = new List<KeyValuePair<string, T>>();

            foreach (var item in data)
            {
                var parsed = _dataParser.ParseModel<T>(modelProps, item);
                result.Add(new KeyValuePair<string, T>(item[Constants.MethodColumnName].ToString(), parsed));
            }
            return result;
        }

        private IEnumerable<Dictionary<string, object>> GetModels<T>(bool useMethodColumn)
        {
            var data = ReadEntitiesFromDb<T>(useMethodColumn);
            var modelProps = new List<string>();
            if (useMethodColumn)
            {
                modelProps.Add(Constants.MethodColumnName);
            }

            modelProps.AddRange(GetModelPropNamesOrdered<T>());

            var result = new List<Dictionary<string, object>>();

            foreach (var valuesArr in data)
            {
                var dict = new Dictionary<string, object>();

                for (var i = 0; i < modelProps.Count(); i++)
                {
                    dict.Add(modelProps[i], valuesArr[i]);
                }
                result.Add(dict);
            }
            return result;
        }

        private List<object[]> ReadEntitiesFromDb<T>(bool useMethodColumn)
        {
            var modelProps = GetModelProps<T>();
            var query = MakeSqlQuery<T>(modelProps, useMethodColumn);
            var columnsCount = useMethodColumn ? modelProps.Count() + 1 : modelProps.Count();

            var result = new List<object[]>();

            using (var connection = new SqlConnection(_options.SqlConnectionString))
            {
                var command = new SqlCommand(query, connection);
                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    object[] values = new object[columnsCount];
                    var n = reader.GetValues(values);
                    result.Add(values);
                }
                reader.Close();
            }
            return result;
        }

        private string MakeSqlQuery<T>(IEnumerable<PropertyInfo> modelProps, bool useMethodColumn)
        {
            modelProps = GetModelProps<T>();

            var sb = new StringBuilder($"SELECT ");

            if (useMethodColumn)
            {
                sb.Append($"{MethodColumnHeader},");
            }

            var fields = string.Join(",", modelProps.OrderBy(m => m.Name).Select(m => m.Name));
            sb.AppendLine(fields);
            sb.AppendLine($" FROM {typeof(T).Name}");

            var query = sb.ToString();
            return query;
        }
    }
}
