using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Types.Registry
{
    public class OperationExceptionInfo
    {
        public string Message { get; set; }

        public OperationExceptionInfo() { }

        public OperationExceptionInfo(string message) { Message = message; }
    }
}
