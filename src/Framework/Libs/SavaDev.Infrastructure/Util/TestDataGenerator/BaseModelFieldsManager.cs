using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Util.TestDataGenerator
{
    public class BaseModelFieldsManager
    {
        protected const string NullableTypeNamePart = "Nullable`1";

        // Important! This is a part of the custom namespase. So that enums which are parsing must be placed inside a namespace containing an "Enums" word.
        protected const string EnumTypeNamePart = "Enums";

        // Important! Value for excluding navigation properties. GetProperties() excludes inherited properties (i.e. Id, IsDeleted) if filter by !field.GetGetMethod().IsVirtual
        protected const string EntityTypeNamePart = "Entities";

        protected const string IdPropertyName = "Id";
        protected const string MethodColumnHeader = "MTDKEY";
        protected const string PrimaryKeyColumnHeader = "PK";

        protected readonly ITestDataProvider _dataGenerator;
        protected readonly ITestDataParser _dataParser;

        public BaseModelFieldsManager(ITestDataProvider dataGenerator)
        {
            _dataGenerator = dataGenerator;
        }

        public BaseModelFieldsManager(ITestDataParser dataParser)
        {
            _dataParser = dataParser;
        }


        public IEnumerable<PropertyInfo> GetModelProps<T>()
        {
            var modelProps = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                .Where(f => !f.PropertyType.FullName.Contains(EntityTypeNamePart)); // See Important! comment in the base class //.Where(f => !f.GetGetMethod().IsVirtual);

            return modelProps;
        }

        public IEnumerable<string> GetModelPropNamesOrdered<T>()
        {
            var modelProps = GetModelProps<T>().Select(m => m.Name).OrderBy(m => m);
            return modelProps;
        }

        public object? GetValue<T>(T model, string propName)
        {
            var value = typeof(T)?.GetProperty(propName)?.GetValue(model, null);
            return value;
        }
    }
}
