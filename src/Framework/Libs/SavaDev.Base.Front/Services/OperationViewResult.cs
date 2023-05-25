using SavaDev.Base.Data.Models.Interfaces;
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

        public OperationViewResult(object processedObject) : this(1)
        {
            ProcessedObjects.Add(processedObject);
        }

        public T? GetProcessedObject<T>() where T : class
        {
            if (!ProcessedObjects.Any()) return null;

            var obj = ProcessedObjects.First() as T;
            return obj;
        }

        public object? GetProcessedObjectId<T>() where T: class, IModel<long>
        {
            var obj = GetProcessedObject<T>();
            if(obj == null) return null;

            return obj.Id;
        }
    }
}
