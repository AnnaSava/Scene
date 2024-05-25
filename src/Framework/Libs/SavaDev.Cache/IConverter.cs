using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Cache
{
    public interface IConverter
    {
        T FromCacheType<T>(object input);

        object ToCacheType<T>(T input);
    }
}
