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

        [Obsolete]
        public object? ProcessedObject { get; }

        public IList<object>? ProcessedObjects { get; set; } = new List<object>();

        [Obsolete]
        public object? Model { get; }

        [Obsolete]
        public (int, object?) Details { get { return (Rows, ProcessedObject); } }

        public List<object> ModelIds { get; set; } = new List<object>();

        public List<OperationExceptionInfo> Exceptions { get; set; } = new List<OperationExceptionInfo>();

        public OperationResult(OperationResult res)
        {
            Rows = res.Rows;
            Exceptions = res.Exceptions;
            ModelIds = res.ModelIds;
        }

        public OperationResult(int rows) { Rows = rows; }

        public OperationResult(IFormModel processedObject)
        {
            Rows = 1;
            ProcessedObject = processedObject;
            ProcessedObjects.Add(processedObject);
        }

        public OperationResult(int rows, IFormModel processedObject)
            : this(rows)
        {
            ProcessedObject = processedObject;
            ProcessedObjects.Add(processedObject);
        }

        public OperationResult(int rows, object processedObject)
           : this(rows)
        {
            ProcessedObject = processedObject;
            ProcessedObjects.Add(processedObject);
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
            ProcessedObject = processedObject;
            ProcessedObjects.Add(processedObject);
            Exceptions.Add(ex);
        }

        public OperationResult(DbOperationRows rows, object processedObject, OperationExceptionInfo ex)
        {
            Rows = (int)rows;
            ProcessedObject = processedObject;
            ProcessedObjects.Add(processedObject);
            Exceptions.Add(ex);
        }

        public string GetExceptionsString()
        {
            var strings = Exceptions.Select(m => m.Message);
            return string.Join('\n', strings);
        }

        public T? GetProcessedObject<T>() where T : class
        {
            if (!ProcessedObjects.Any()) return null;

            var obj = ProcessedObjects.First() as T;
            return obj;
        }
    }

    public class OperationResult<TModel> : OperationResult
    {
        public List<TModel> Models { get; set; } = new List<TModel>();

        public TModel Model { get { return Models.First(); } }

        public object Model0 { get { return Models.First(); } }

        public OperationResult(int rows, TModel model)
            : base(rows)
        {
            Models.Add(model);
        }

        public OperationResult(int rows, IEnumerable<TModel> models)
            : base(rows)
        {
            Models.AddRange(models);
        }

        public OperationResult(int rows, TModel model, OperationExceptionInfo ex)
            : base(rows)
        {
            Models.Add(model);
            Exceptions.Add(ex);
        }

        public OperationResult(OperationResult res) : base(res)
        {
        }
    }
}
