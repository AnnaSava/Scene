using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Models.Interfaces;
using SavaDev.Base.Data.Services;
using System.Linq.Expressions;

namespace SavaDev.Base.Data.Managers.Crud
{
    public class EntityUpdater<TKey, TEntity>
    {
        #region Private Fields: Dependencies

        protected readonly IDbContext _dbContext;
        protected readonly ILogger _logger;

        #endregion

        #region Private Properties: Func

        private Func<IFormModel, Task>? OnValidating { get; set; }
        private Func<TEntity, Task>? OnValidatingEntity { get; set; }
        private Func<TKey, Task<TEntity>> OnGetToUpdate { get; set; }
        private Func<Expression<Func<TEntity, bool>>, Task<TEntity>> OnGetToUpdateExp { get; set; }
        private Func<TKey, Task<TEntity>> OnGetToRestore { get; set; }
        private Func<TEntity, Task> OnSetValues { get; set; }
        private Func<TEntity, IFormModel, Task> OnSetValuesFromModel { get; set; }
        private Func<TEntity, Task<OperationResult>> OnUpdate { get; set; }
        private Func<TEntity, IFormModel, Task<OperationResult>> OnAfterUpdate { get; set; }
        private Func<TEntity, OperationResult> OnSuccess { get; set; }
        private Func<TKey?, string, OperationResult> OnError { get; set; }
        private Func<string, OperationResult> OnErrorNoKey { get; set; }

        #endregion

        #region Public Constructors

        public EntityUpdater(IDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #endregion

        #region Public Methods: Set

        public EntityUpdater<TKey, TEntity> ValidateModel(Func<IFormModel, Task> func)
        {
            OnValidating = func;
            return this;
        }

        public EntityUpdater<TKey, TEntity> GetEntity(Func<TKey, Task<TEntity>> func)
        {
            OnGetToUpdate = func;
            return this;
        }

        public EntityUpdater<TKey, TEntity> GetEntity(Func<Expression<Func<TEntity, bool>>, Task<TEntity>> func)
        {
            OnGetToUpdateExp = func;
            return this;
        }

        public EntityUpdater<TKey, TEntity> ValidateEntity(Func<TEntity, Task> func)
        {
            OnValidatingEntity = func;
            return this;
        }

        public EntityUpdater<TKey, TEntity> SetValues(Func<TEntity, Task> func)
        {
            OnSetValues = func;
            return this;
        }

        public EntityUpdater<TKey, TEntity> SetValues(Func<TEntity, IFormModel, Task> func)
        {
            OnSetValuesFromModel = func;
            return this;
        }

        public EntityUpdater<TKey, TEntity> Update(Func<TEntity, Task<OperationResult>> func)
        {
            OnUpdate = func;
            return this;
        }

        public EntityUpdater<TKey, TEntity> AfterUpdate(Func<TEntity, IFormModel, Task<OperationResult>> func)
        {
            OnAfterUpdate = func;
            return this;
        }

        public EntityUpdater<TKey, TEntity> SuccessResult(Func<TEntity, OperationResult> func)
        {
            OnSuccess = func;
            return this;
        }

        public EntityUpdater<TKey, TEntity> ErrorResult(Func<TKey?, string, OperationResult> func)
        {
            OnError = func;
            return this;
        }

        public EntityUpdater<TKey, TEntity> ErrorResult(Func<string, OperationResult> func)
        {
            OnErrorNoKey = func;
            return this;
        }

        #endregion

        #region Public Methods: Act

        public async Task<OperationResult> DoUpdate<TFormModel>(Expression<Func<TEntity, bool>> expression, IFormModel model)
        {
            if (model != null)
            {
                await DoValidate(model);
            }

            using var tran = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var entity = await DoGetEntityForUpdate(expression);
                var result = await ProcessUpdate(entity, model);
                await tran.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError($"{nameof(DoUpdate)}: {ex.Message} {ex.InnerException?.Message} {ex.StackTrace}");
                var result = DoOnError(ex.Message);
                result.Rows = -1; // TODO раз присваиваем здесь, то выпилить из конструктора OperationResultи вызовов методов
                return result;
            }
        }


        public async Task<OperationResult> DoUpdate<TFormModel>(TKey id, IFormModel model)
        {
            var result = await DoUpdate(id, false, model);
            return result;
        }

        public async Task<OperationResult> DoUpdate(TKey id, bool restore = false)
        {
            var result = await DoUpdate(id, restore);
            return result;
        }

        public async Task<OperationResult> DoUpdate(TKey id, bool restore, IFormModel? model = null)
        {
            if (restore && model != null)
                throw new InvalidOperationException("Cannot restore and update at one time!");

            if (model != null)
            {
                await DoValidate(model);
            }
            
            using var tran = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var entity = restore ? await DoGetEntityForRestore(id) : await DoGetEntityForUpdate(id);
                var result = await ProcessUpdate(entity, model);
                await tran.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError($"{nameof(DoUpdate)}: {ex.Message} {ex.InnerException?.Message} {ex.StackTrace}");
                var result = DoOnError(id, ex.Message);
                result.Rows = -1; // TODO раз присваиваем здесь, то выпилить из конструктора OperationResultи вызовов методов
                return result;
            }
        }

        #endregion

        #region Protected Methods : Act

        protected virtual Task DoValidate(IFormModel model)
        {
            var task = OnValidating?.Invoke(model);
            return task ?? Task.CompletedTask;
        }

        protected virtual Task DoValidate(TEntity entity)
        {
            var task = OnValidatingEntity?.Invoke(entity);
            return task ?? Task.CompletedTask;
        }

        protected virtual Task<TEntity> DoGetEntityForUpdate(TKey id)
        {
            var task = OnGetToUpdate?.Invoke(id);
            return task ?? (Task<TEntity>)Task.CompletedTask;
        }

        protected virtual Task<TEntity> DoGetEntityForUpdate(Expression<Func<TEntity, bool>> expression)
        {
            var task = OnGetToUpdateExp?.Invoke(expression);
            return task ?? (Task<TEntity>)Task.CompletedTask;
        }

        protected virtual Task<TEntity> DoGetEntityForRestore(TKey id)
        {
            var task = OnGetToRestore?.Invoke(id);
            return task ?? (Task<TEntity>)Task.CompletedTask;
        }

        protected virtual Task DoSetValues(TEntity entity)
        {
            var task = OnSetValues?.Invoke(entity);
            return task ?? Task.CompletedTask;
        }

        protected virtual Task DoSetValuesFromModel(TEntity entity, IFormModel model)
        {
            var task = OnSetValuesFromModel?.Invoke(entity, model);
            return task ?? Task.CompletedTask;
        }

        protected virtual Task<OperationResult> DoUpdate(TEntity entity)
        {
            var task = OnUpdate?.Invoke(entity);
            return task ?? (Task<OperationResult>)Task.CompletedTask;
        }

        protected virtual Task<OperationResult> DoOnAfterUpdate(TEntity entity, IFormModel model)
        {
            var task = OnAfterUpdate?.Invoke(entity, model);
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

        private async Task<OperationResult> ProcessUpdate(TEntity entity, IFormModel model)
        {
            await DoValidate(entity);
            if (model != null)
            {
                await DoSetValuesFromModel(entity, model);
            }
            else
            {
                await DoSetValues(entity);
            }
            var rows = 0;
            var updateResult = await DoUpdate(entity);
            rows += HandleResult(updateResult, nameof(DoUpdate));
            if (model != null)
            {
                var afterUpdateResult = await DoOnAfterUpdate(entity, model);
                rows += HandleResult(afterUpdateResult, nameof(DoOnAfterUpdate));
            }
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
