using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Util.ModelFieldsManager
{
    public interface IModelFieldsReader
    {
        public List<T> GetModels<T>() where T : class, new();

        public List<KeyValuePair<string, T>> GetModelsWithTestMethods<T>() where T : class, new();
    }
}
