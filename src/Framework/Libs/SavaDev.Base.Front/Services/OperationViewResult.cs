using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Front.Services
{
    public class OperationViewResult
    {
        public int Rows { get; set; }

        public bool IsSuccess { get { return Rows > 0; } }

        public bool NotChanged { get { return Rows == 0; } }

        public object? ProcessedObject { get; }

        public OperationViewResult(int rows) { Rows = rows; }

        public OperationViewResult(int rows, object processedObject) : this(rows)
        {
            ProcessedObject = processedObject;
        }

        public OperationViewResult((int, object) details)
        {
            Rows = details.Item1;
            ProcessedObject= details.Item2;
        }
    }
}
