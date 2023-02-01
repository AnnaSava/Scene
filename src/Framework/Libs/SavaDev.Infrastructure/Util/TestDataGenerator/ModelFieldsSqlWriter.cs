using System.Data.SqlClient;
using System.Text;

namespace SavaDev.Infrastructure.Util.TestDataGenerator
{
    public class ModelFieldsSqlWriter : BaseModelFieldsManager
    {
        private readonly ModelFieldsSqlWriterOptions _options;

        public ModelFieldsSqlWriter(ModelFieldsSqlWriterOptions options, ITestDataProvider dataProvider) : base(dataProvider)
        {
            _options = options;
        }

        public void WriteTestDataToDb<T>(int rowsCount)
            where T : class, new()
        {
            DropTable<T>();
            CreateTable<T>();

            var pk = 1;

            if (_options.UseMethodNameColumn)
            {
                var data = _dataGenerator.ProvideTestDataWithMethodColumn<T>(rowsCount);
                foreach (var item in data)
                {
                    InsertIntoTable(item.Value, pk++, item.Key);
                }
            }
            else
            {
                var data = _dataGenerator.ProvideTestData<T>(rowsCount);
                foreach (var item in data)
                {
                    InsertIntoTable(item, pk++);
                }
            }
        }

        private void DropTable<T>()
        {
            var query = $"DROP TABLE IF EXISTS [{typeof(T).Name}]";
            Exec(query);
        }

        private void CreateTable<T>()
        {
            var modelProps = GetModelProps<T>();

            var sb = new StringBuilder($"CREATE TABLE [{typeof(T).Name}] (");
            sb.AppendLine($"{PrimaryKeyColumnHeader} int,");
            if (_options.UseMethodNameColumn)
            {
                sb.AppendLine($"[{MethodColumnHeader}] NVARCHAR(255),");
            }
            foreach (var prop in modelProps)
            {
                sb.AppendLine($"[{prop.Name}] NVARCHAR(255),");
            }
            sb.AppendLine("PRIMARY KEY (PK)");
            sb.AppendLine(");");

            var query = sb.ToString();
            Exec(query);
        }

        private void InsertIntoTable<T>(T model, int pk, string methodName = null, bool useMethodColumn = false)
        {
            var modelProps = GetModelProps<T>().OrderBy(m => m.Name);

            var sb = new StringBuilder($"INSERT INTO [{typeof(T).Name}] ({PrimaryKeyColumnHeader},");
            if (useMethodColumn)
            {
                sb.Append($"{MethodColumnHeader},");
            }

            var fieldsStr = string.Join(",", modelProps.Select(m => $"[{m.Name}]"));
            sb.AppendLine(fieldsStr);
            sb.AppendLine(")");
            sb.AppendLine($"VALUES ({pk},");
            if (useMethodColumn)
            {
                var methodNameVal = methodName == null ? "NULL" : methodName;
                sb.Append($"{methodNameVal},");
            }

            var c = 1;
            foreach (var item in modelProps)
            {
                var value = GetValue(model, item.Name);

                sb.Append(value == null ? "NULL" : $"'{value}'");
                if (c != modelProps.Count())
                {
                    sb.Append(',');
                }
                c++;
            }
            sb.Append(")");

            var query = sb.ToString();
            Exec(query);
        }

        private void Exec(string query)
        {
            using (var connection = new SqlConnection(_options.SqlConnectionString))
            {
                var command = new SqlCommand(query, connection);
                connection.Open();

                command.ExecuteNonQuery();
            }
        }
    }
}
