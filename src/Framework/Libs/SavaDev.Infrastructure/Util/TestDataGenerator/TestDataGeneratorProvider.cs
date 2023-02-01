using SavaDev.Infrastructure.Util.TestDataGenerator;
using System.Reflection;

namespace SavaDev.Infrastructure.Util.TestDataGenerator
{
    public class TestDataGeneratorProvider : ITestDataProvider
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

        private readonly BaseModelFieldsManager _fieldsManager;
        private readonly ITestDataParser _dataParser;

        public TestDataGeneratorProvider(BaseModelFieldsManager fieldsManager, ITestDataParser dataParser)
        {
            Types = Defaults.Select(m => m.Key);
            _fieldsManager = fieldsManager;
            _dataParser = dataParser;
        }

        public IEnumerable<T> ProvideTestData<T>(int rowsCount)
            where T : class, new()
        {
            var data = ProvideTestData<T>(useMethodColumn: false, rowsCount);
            var modelProps = _fieldsManager.GetModelProps<T>();

            var result = new List<T>();

            foreach (var model in data)
            {
                var parsed =  _dataParser.ParseModel<T>(modelProps, model);
                result.Add(parsed);
            }
            return result;
        }

        public IEnumerable<KeyValuePair<string, T>> ProvideTestDataWithMethodColumn<T>(int rowsCount)
            where T : class, new()
        {
            var data = ProvideTestData<T>(useMethodColumn: true, rowsCount);
            var modelProps = _fieldsManager.GetModelProps<T>();

            var result = new List<KeyValuePair<string, T>>();

            foreach (var modelDict in data)
            {
                var parsed = _dataParser.ParseModel<T>(modelProps, modelDict);
                var methodName = GetMethodName(modelDict);

                result.Add(new KeyValuePair<string, T>(methodName, parsed));
            }
            return result;
        }

        private IEnumerable<Dictionary<string, object>> ProvideTestData<T>(bool useMethodColumn, int rowsCount)
            where T : class, new()
        {
            var modelProps = _fieldsManager.GetModelProps<T>();
            var list = new List<Dictionary<string, object>>();

            for (var i = 0; i < rowsCount; i++)
            {
                var dict = new Dictionary<string, object>();

                if (useMethodColumn)
                {
                    dict.Add(Constants.MethodColumnName, null);
                }

                foreach (var prop in modelProps)
                {
                    dict.Add(prop.Name, null);
                    var idNumber = i + 1;

                    if (prop.Name == Constants.IdPropertyName)
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

        public void FillDataByTypes(Dictionary<string, object> modelDict, PropertyInfo prop, int idNumber)
        {
            var random = new Random();
            foreach (var typeName in Types)
            {
                if (IsType(prop, typeName))
                {
                    if (IsFkType(prop, typeName))
                    {
                        modelDict[prop.Name] = idNumber.ToString();
                        continue;
                    }

                    modelDict[prop.Name] = Defaults[typeName]();

                    if (typeName == nameof(String))
                    {
                        modelDict[prop.Name] += $"{idNumber} {random.Next(idNumber, idNumber + 5)}";
                    }
                    continue;
                }
            }
            if (IsEnum(prop))
            {
                modelDict[prop.Name] = 0;
            }
        }

        public bool IsEnum(PropertyInfo prop)
        {
            return prop.PropertyType.FullName.Contains(Constants.EnumTypeNamePart);
        }

        public bool IsType(PropertyInfo prop, string typeName)
        {
            return prop.PropertyType.FullName.Contains(typeName);
        }

        public bool IsFkType(PropertyInfo prop, string typeName)
        {
            return typeName == nameof(String) && prop.Name.EndsWith(Constants.IdPropertyName);
        }

        public string GetMethodName(Dictionary<string, object> modelDict)
        {
            return modelDict[Constants.MethodColumnName] == null ? null : modelDict[Constants.MethodColumnName].ToString();
        }
    }
}
