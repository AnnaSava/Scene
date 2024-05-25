using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Cache
{
    public class JsonTypeConverter : IConverter
    {
        public T FromCacheType<T>(object input)
        {
            var serializedValue = input.ToString();
            var result = JsonConvert.DeserializeObject<T>(serializedValue);
            return result;
        }

        public object ToCacheType<T>(T input)
        {
            string serializedValue = JsonConvert.SerializeObject(input);
            return serializedValue;
        }
    }
}
