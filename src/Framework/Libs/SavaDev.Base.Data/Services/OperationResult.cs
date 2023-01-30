using System;
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

        public List<object> ModelIds { get; set; } = new List<object>();

        public List<OperationExceptionInfo> Exceptions { get; set; } = new List<OperationExceptionInfo>();

        public OperationResult(OperationResult res)
        {
            Rows = res.Rows;
            Exceptions = res.Exceptions;
            ModelIds = res.ModelIds;
        }

        public OperationResult(int rows) { Rows = rows; }

        public OperationResult(int rows, object modelId)
            : this(rows)
        {
            ModelIds.Add(modelId);
        }

        public OperationResult(int rows, IEnumerable<object> modelIds)
            : this(rows)
        {
            ModelIds.AddRange(modelIds);
        }

        public OperationResult(int rows, OperationExceptionInfo ex)
            : this(rows)
        {
            Exceptions.Add(ex);
        }

        public OperationResult(int rows, object id, OperationExceptionInfo ex)
            : this(rows)
        {
            Exceptions.Add(ex);
            ModelIds.Add(id);
        }
    }

    public class OperationResult<TModel> : OperationResult
    {
        public List<TModel> Models { get; set; } = new List<TModel>();

        public TModel Model { get { return Models.First(); } }

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
