using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Services
{
    public class OperationExceptionInfo
    {
        public string Message { get; set; }

        public OperationExceptionInfo() { }

        public OperationExceptionInfo(string message) { Message = message; }
    }
}
