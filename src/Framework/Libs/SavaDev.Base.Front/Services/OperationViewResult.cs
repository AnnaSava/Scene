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

        public IList<object>? ProcessedObjects { get; set; } = new List<object>();

        public OperationViewResult(int rows) { Rows = rows; }

        public OperationViewResult(int rows, object processedObject) : this(rows)
        {
            ProcessedObjects.Add(processedObject);
        }

        [Obsolete]
        public OperationViewResult((int, object) details)
        {
            Rows = details.Item1;
            ProcessedObjects.Add(details.Item2);
        }

        public T? GetProcessedObject<T>() where T : class
        {
            if (!ProcessedObjects.Any()) return null;

            var obj = ProcessedObjects.First() as T;
            return obj;
        }
    }
}
