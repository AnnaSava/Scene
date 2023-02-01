using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Util.TestDataGenerator
{
    public static class Constants
    {
        public const string IdPropertyName = "Id";

        public const string NullableTypeNamePart = "Nullable`1";

        // Important! This is a part of the custom namespase. So that enums which are parsing must be placed inside a namespace containing an "Enums" word.
        public const string EnumTypeNamePart = "Enums";

        // Important! Value for excluding navigation properties. GetProperties() excludes inherited properties (i.e. Id, IsDeleted) if filter by !field.GetGetMethod().IsVirtual
        public const string EntityTypeNamePart = "Entities";

        public const string MethodColumnName = "MTDKEY";
    }
}
