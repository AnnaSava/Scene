using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Util.TestDataGenerator
{
    public interface ITestDataParser
    {
        T ParseModel<T>(IEnumerable<PropertyInfo> eProps, Dictionary<string, string> input) where T : class, new();

        T ParseModel<T>(IEnumerable<PropertyInfo> eProps, Dictionary<string, object> input) where T : class, new();
    }
}
