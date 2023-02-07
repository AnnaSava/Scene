using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Services;
using System.Linq.Expressions;

namespace SavaDev.Base.Data.Managers.Crud
{
    public class EntityRemover<TKey, TEntity>
    {
        #region Private Fields: Dependencies

        protected readonly IDbContext _dbContext;
        protected readonly ILogger _logger;

        #endregion

        #region Private Properties: Func

        private Func<TKey, Task<TEntity>> OnGetToRemove { get; set; }
        private Func<Expression<Func<TEntity, bool>>, Task<TEntity>> OnGetToRemoveExp { get; set; }
        private Func<TEntity, Task<OperationResult>> OnRemove { get; set; }
        private Func<TEntity, Task<OperationResult>> OnAfterRemove { get; set; }
        private Func<TEntity, OperationResult> OnSuccess { get; set; }
        private Func<TKey?, string, OperationResult> OnError { get; set; }
        private Func<string, OperationResult> OnErrorNoKey { get; set; }

        #endregion

        #region Public Constructors

        public EntityRemover(IDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #endregion

        #region Public Methods: Set

        public EntityRemover<TKey, TEntity> GetEntity(Func<TKey, Task<TEntity>> func)
        {
            OnGetToRemove = func;
            return this;
        }

        public EntityRemover<TKey, TEntity> GetEntity(Func<Expression<Func<TEntity, bool>>, Task<TEntity>> func)
        {
            OnGetToRemoveExp = func;
            return this;
        }

        public EntityRemover<TKey, TEntity> Remove(Func<TEntity, Task<OperationResult>> func)
        {
            OnRemove = func;
            return this;
        }

        public EntityRemover<TKey, TEntity> AfterRemove(Func<TEntity, Task<OperationResult>> func)
        {
            OnAfterRemove = func;
            return this;
        }

        public EntityRemover<TKey, TEntity> SuccessResult(Func<TEntity, OperationResult> func)
        {
            OnSuccess = func;
            return this;
        }

        public EntityRemover<TKey, TEntity> ErrorResult(Func<TKey?, string, OperationResult> func)
        {
            OnError = func;
            return this;
        }

        public EntityRemover<TKey, TEntity> ErrorResult(Func<string, OperationResult> func)
        {
            OnErrorNoKey = func;
            return this;
        }

        #endregion

        #region Public Methods: Act

        public async Task<OperationResult> DoRemove(Expression<Func<TEntity, bool>> expression)
        {
            var rows = 0;
            using var tran = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var entity = await DoGetEntityForRemove(expression);
                var result = await ProcessRemove(entity); 
                await tran.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError($"{nameof(DoRemove)}: {ex.Message} {ex.InnerException?.Message} {ex.StackTrace}");
                var result = DoOnError(ex.Message);
                result.Rows = -1; // TODO раз присваиваем здесь, то выпилить из конструктора OperationResultи вызовов методов
                return result;
            }
        }

        public async Task<OperationResult> DoRemove(TKey id)
        {

            using var tran = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var entity = await DoGetEntityForRemove(id);
                var result = await ProcessRemove(entity); ;
                await tran.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError($"{nameof(DoRemove)}: {ex.Message} {ex.InnerException?.Message} {ex.StackTrace}");
                var result = DoOnError(id, ex.Message);
                result.Rows = -1; // TODO раз присваиваем здесь, то выпилить из конструктора OperationResultи вызовов методов
                return result;
            }
        }

        #endregion

        #region Protected Methods : Act

        protected virtual Task<TEntity> DoGetEntityForRemove(TKey id)
        {
            var task = OnGetToRemove?.Invoke(id);
            return task ?? (Task<TEntity>)Task.CompletedTask;
        }

        protected virtual Task<TEntity> DoGetEntityForRemove(Expression<Func<TEntity, bool>> expression)
        {
            var task = OnGetToRemoveExp?.Invoke(expression);
            return task ?? (Task<TEntity>)Task.CompletedTask;
        }

        protected virtual Task<OperationResult> DoRemove(TEntity entity)
        {
            var task = OnRemove?.Invoke(entity);
            return task ?? (Task<OperationResult>)Task.CompletedTask;
        }

        protected virtual Task<OperationResult> DoOnAfterRemove(TEntity entity)
        {
            var task = OnAfterRemove?.Invoke(entity);
            return task ?? (Task<OperationResult>)Task.CompletedTask;
        }

        protected virtual OperationResult DoOnSuccess(TEntity entity)
        {
            var result = OnSuccess?.Invoke(entity);
            return result ?? new OperationResult(0);
        }

        protected virtual OperationResult DoOnError(TKey? id, string errMessage)
        {
            var result = OnError?.Invoke(id, errMessage);
            return result ?? new OperationResult(-1);
        }

        protected virtual OperationResult DoOnError(string errMessage)
        {
            var result = OnErrorNoKey?.Invoke(errMessage);
            return result ?? new OperationResult(-1);
        }

        #endregion

        #region Private Methods

        private async Task<OperationResult> ProcessRemove(TEntity entity)
        {
            var rows = 0;
            var removeResult = await DoRemove(entity);
            rows += HandleResult(removeResult, nameof(DoRemove));

            var afterRemoveResult = await DoOnAfterRemove(entity);
            rows += HandleResult(afterRemoveResult, nameof(DoOnAfterRemove));

            var result = DoOnSuccess(entity);
            result.Rows = rows; // TODO раз присваиваем здесь, то выпилить из конструктора OperationResultи вызовов методов
            return result;
        }

        private int HandleResult(OperationResult result, string methodName)
        {
            if (!result.IsSuccess)
            {
                throw new Exception($"Operation in {methodName} failed", new Exception(result.GetExceptionsString()));
            }
            return result.Rows;
        }

        #endregion
    }
}
