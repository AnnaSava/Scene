using System.Reflection;

namespace SavaDev.Infrastructure.Util.TestDataGenerator
{
    public class ModelFieldsCsvReader : BaseModelFieldsManager, IModelFieldsReader
    {
        private readonly ModelFieldsCsvReaderOptions _options;

        public ModelFieldsCsvReader(ModelFieldsCsvReaderOptions options, ITestDataParser dataParser) : base(dataParser)
        {
            _options = options;
        }

        public List<T> GetModels<T>()
            where T : class, new()
        {
            // Example: "goalformmodel_123.csv"
            var searchPattern = $"{typeof(T).Name}_*.csv";

            var files = Directory.GetFiles(_options.InputFolderName, searchPattern);

            if (files == null || !files.Any()) return null;

            var entities = new List<T>();
            var modelProps = GetModelProps<T>();

            foreach (var file in files)
            {
                var data = ReadCsvFile(file);

                foreach (var item in data.Skip(1))
                {
                    var entity = _dataParser.ParseModel<T>(modelProps, item);

                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<KeyValuePair<string, T>> GetModelsWithTestMethods<T>()
            where T : class, new()
        {            
            // Example: "goalformmodel_123.csv"
            var searchPattern = $"{typeof(T).Name}_*.csv";

            var files = Directory.GetFiles(_options.InputFolderName, searchPattern);

            if (files == null || !files.Any()) return null;

            var models = new List<KeyValuePair<string, T>>();
            var modelProps = GetModelProps<T>();

            foreach (var file in files)
            {
                var data = ReadCsvFile(file);

                foreach (var item in data.Skip(1))
                {
                    var testName = item[MethodColumnHeader];
                    var model = _dataParser.ParseModel<T>(modelProps, item);

                    models.Add(new KeyValuePair<string, T>(testName, model));
                }

            }
            return models;
        }

        public IEnumerable<Dictionary<string, string>> ReadCsvFile(string fileName)
        {
            var strings = new List<string>();
            using (var sr = new StreamReader(fileName))
            {
                while (sr.Peek() >= 0)
                {
                    strings.Add(sr.ReadLine());
                }
            }
            return GetCsvDictionaries(strings);
        }

        public IEnumerable<Dictionary<string, string>> GetCsvDictionaries(IEnumerable<string> strings)
        {
            if (strings == null || !strings.Any()) return null;

            var stringsArr = strings.ToArray();
            var fieldNames = stringsArr[0].Split(';');

            var fields = new List<Dictionary<string, string>>();

            foreach (var str in strings)
            {
                var dict = new Dictionary<string, string>();

                var valsArr = str.Split(_options.CsvSeparator);
                for (var i = 0; i < fieldNames.Length; i++)
                {
                    dict.Add(fieldNames[i], valsArr[i]);
                }

                fields.Add(dict);
            }
            return fields;
        }        
    }
}
