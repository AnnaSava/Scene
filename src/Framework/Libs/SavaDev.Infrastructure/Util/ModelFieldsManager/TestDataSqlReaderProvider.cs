using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SavaDev.Infrastructure.Util.ModelFieldsManager
{
    public class TestDataSqlReaderProvider : ITestDataProvider
    {
        private ModelFieldsSqlReader _reader;

        public TestDataSqlReaderProvider(ModelFieldsSqlReader reader)
        {
            _reader = reader;
        }

        public IEnumerable<T> ProvideTestData<T>(int rowsCount)
            where T : class, new()
        {
            var data = _reader.GetModels<T>();
            return data;
        }

        public IEnumerable<KeyValuePair<string, T>> ProvideTestDataWithMethodColumn<T>(int rowsCount)
            where T : class, new()
        {
            var data = _reader.GetModelsWithTestMethods<T>();
            return data;
        }
    }
}
