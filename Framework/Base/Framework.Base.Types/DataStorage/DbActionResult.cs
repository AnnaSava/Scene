using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Types.DataStorage
{
    public enum DbActionStatus
    {
        Succeeded,
        Failed
    }

    public class DbActionResult<T>
    {
        public DbActionStatus Status { get; set; }

        public T Data { get; set; }

        public List<string> Messages { get; set; }
    }
}
