using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Util.TestDataGenerator
{
    public interface ITestDataProvider
    {
        IEnumerable<T> ProvideTestData<T>(int rowsCount) where T : class, new();

        IEnumerable<KeyValuePair<string, T>> ProvideTestDataWithMethodColumn<T>(int rowsCount) where T : class, new();
    }
}
