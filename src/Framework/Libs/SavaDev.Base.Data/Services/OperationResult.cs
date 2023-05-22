using SavaDev.Base.Data.Enums;
using SavaDev.Base.Data.Models.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Services
{
    public class OperationResult
    {
        public int Rows { get; set; }

        public bool IsSuccess { get { return Rows > 0; } }

        public bool NotChanged { get { return Rows == 0; } }

        public IList<object>? ProcessedObjects { get; set; } = new List<object>();

        public List<object> ModelIds { get; set; } = new List<object>();

        public List<OperationExceptionInfo> Exceptions { get; set; } = new List<OperationExceptionInfo>();

        public OperationResult(OperationResult res)
        {
            Rows = res.Rows;
            Exceptions = res.Exceptions;
            ModelIds = res.ModelIds;
        }

        public OperationResult(int rows) { Rows = rows; }

        public OperationResult(IFormModel processedObject) : this(1)
        {
            ProcessedObjects?.Add(processedObject);
        }

        public OperationResult(int rows, IFormModel processedObject)
            : this(rows)
        {
            ProcessedObjects?.Add(processedObject);
        }

        public OperationResult(int rows, object processedObject)
           : this(rows)
        {           
            ProcessedObjects?.Add(processedObject);
        }

        public OperationResult(int rows, IEnumerable<object> modelIds)
            : this(rows)
        {
            ModelIds.AddRange(modelIds);
        }

        public OperationResult(int rows, IFormModel processedObject, OperationExceptionInfo ex)
            : this(rows, processedObject)
        {
            Exceptions.Add(ex);
        }

        public OperationResult(int rows, object processedObject, OperationExceptionInfo ex)
            : this(rows)
        {
            ProcessedObjects?.Add(processedObject);
            Exceptions.Add(ex);
        }

        public OperationResult(DbOperationRows rows, object processedObject, OperationExceptionInfo ex)
        {
            Rows = (int)rows;
            ProcessedObjects?.Add(processedObject);
            Exceptions.Add(ex);
        }

        public string GetExceptionsString()
        {
            var strings = Exceptions.Select(m => m.Message);
            return string.Join('\n', strings);
        }

        public T? GetProcessedObject<T>() where T : class
        {
            if (ProcessedObjects == null || !ProcessedObjects.Any()) return null;

            var obj = ProcessedObjects.First() as T;
            return obj;
        }

        public object? GetProcessedObject()
        {
            if (ProcessedObjects == null || !ProcessedObjects.Any()) return null;

            var obj = ProcessedObjects.First();
            return obj;
        }
    }

    [Obsolete]
    public class OperationResult<TModel> : OperationResult
    {
        public List<TModel> Models { get; set; } = new List<TModel>();

        [Obsolete]
        public OperationResult(int rows, TModel model)
            : base(rows)
        {
            Models.Add(model);
        }
    }
}
