using SavaDev.Infrastructure.Util.TestDataGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Util.SchemeManager
{
    public class ModelFieldsObjectManager : BaseModelFieldsManager
    {
        public Dictionary<string, Dictionary<string, string>> GetModelAndProps(string @namespace)
        {
            var modelDict = new Dictionary<string, Dictionary<string, string>>();

            var types = GetModelTypes(@namespace);

            foreach (var type in types)
            {
                var props = GetModelProps(type);
                var propDict = new Dictionary<string, string>();
                foreach (var prop in props)
                {
                    var propertyType = prop.PropertyType.Name.Contains(NullableTypeNamePart) 
                        ? GetTypeNameFromNullableFullName(prop.PropertyType.FullName) + "?" 
                        : prop.PropertyType.Name;
                    propDict.Add(prop.Name, propertyType);
                }
                modelDict.Add(type.Name, propDict);
            }

            return modelDict;
        }

        private string? GetTypeNameFromNullableFullName(string fullName)
        {
            var cleanStart = fullName.Replace("System.Nullable`1[[", "");
            var arr = cleanStart.Split(','); ;
            if(arr.Length !=0)
            {
                var name = arr[0].Substring(arr[0].LastIndexOf(".") + 1);
                return name;
            }
            return null;
        }
    }
}
