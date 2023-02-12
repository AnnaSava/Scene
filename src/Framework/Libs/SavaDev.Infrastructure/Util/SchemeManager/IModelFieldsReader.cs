using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Util.TestDataGenerator
{
    public interface IModelFieldsReader
    {
        public List<T> GetModels<T>(string? directory = null) where T : class, new();

        public List<KeyValuePair<string, T>> GetModelsWithTestMethods<T>(string? directory = null) where T : class, new();
    }
}
