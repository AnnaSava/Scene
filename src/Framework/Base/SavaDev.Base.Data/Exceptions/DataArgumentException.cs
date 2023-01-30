using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Exceptions
{
    public class DataArgumentException : ArgumentException
    {
        public DataArgumentException() { }

        public DataArgumentException(string message) : base(message) { }
    }
}
