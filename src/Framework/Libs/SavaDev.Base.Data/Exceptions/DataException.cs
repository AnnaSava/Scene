using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Exceptions
{
    public class DataException : Exception
    {
        public DataException() { }

        public DataException(string message) : base(message) { }
    }
}
