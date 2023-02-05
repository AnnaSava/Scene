using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Models.Interfaces;
using SavaDev.Base.Data.Services;

namespace SavaDev.Base.Data.Managers.Crud
{
    public class EntityCreator<TKey, TEntity>
    {
        #region Private Fields: Dependencies

        protected readonly IDbContext _dbContext;
        protected readonly ILogger _logger;

        #endregion

        #region Private Properties: Func

        private Func<IFormModel, Task>? OnValidating { get; set; }
        private Func<IFormModel, Task<TEntity>> OnSetValuesFromModel { get; set; }
        private Func<TEntity, Task<OperationResult>> OnCreate{ get; set; }
        private Func<TEntity, IFormModel, Task<OperationResult>> OnAfterCreate { get; set; }
        private Func<TEntity, OperationResult> OnSuccess { get; set; }
        private Func<IFormModel?, string, OperationResult> OnError { get; set; }

        #endregion

        #region Public Constructors

        public EntityCreator(IDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #endregion

        #region Public Methods: Set

        public EntityCreator<TKey, TEntity> ValidateModel(Func<IFormModel, Task> func)
        {
            OnValidating = func;
            return this;
        }

        public EntityCreator<TKey, TEntity> SetValues(Func<IFormModel, Task<TEntity>> func)
        {
            OnSetValuesFromModel = func;
            return this;
        }

        public EntityCreator<TKey, TEntity> Create(Func<TEntity, Task<OperationResult>> func)
        {
            OnCreate = func;
            return this;
        }

        public EntityCreator<TKey, TEntity> AfterCreate(Func<TEntity, IFormModel, Task<OperationResult>> func)
        {
            OnAfterCreate = func;
            return this;
        }

        public EntityCreator<TKey, TEntity> SuccessResult(Func<TEntity, OperationResult> func)
        {
            OnSuccess = func;
            return this;
        }

        public EntityCreator<TKey, TEntity> ErrorResult(Func<IFormModel, string, OperationResult> func)
        {
            OnError = func;
            return this;
        }

        #endregion

        public async Task<OperationResult> DoCreate(IFormModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            await DoValidate(model);

            var rows = 0;
            using var tran = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var entity = await DoSetValuesFromModel(model);
                var createResult = await DoCreate(entity);
                rows += HandleResult(createResult, nameof(DoCreate));

                var afterCreateResult = await DoOnAfterCreate(entity, model);
                rows += HandleResult(afterCreateResult, nameof(DoOnAfterCreate));

                var result = DoOnSuccess(entity);
                result.Rows = rows; // TODO раз присваиваем здесь, то выпилить из конструктора OperationResultи вызовов методов
                await tran.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError($"{nameof(DoCreate)}: {ex.Message} {ex.InnerException?.Message} {ex.StackTrace}");
                var result = DoOnError(model, ex.Message);
                result.Rows = -1; // TODO раз присваиваем здесь, то выпилить из конструктора OperationResultи вызовов методов
                return result;
            }
        }

        #region Protected Methods : Act

        protected virtual Task DoValidate(IFormModel model)
        {
            var task = OnValidating?.Invoke(model);
            return task ?? Task.CompletedTask;
        }

        protected virtual Task<TEntity> DoSetValuesFromModel(IFormModel model)
        {
            var task = OnSetValuesFromModel?.Invoke(model);
            return task ?? (Task<TEntity>)Task.CompletedTask;
        }

        protected virtual Task<OperationResult> DoCreate(TEntity entity)
        {
            var task = OnCreate?.Invoke(entity);
            return task ?? (Task<OperationResult>)Task.CompletedTask;
        }

        protected virtual Task<OperationResult> DoOnAfterCreate(TEntity entity, IFormModel model)
        {
            var task = OnAfterCreate?.Invoke(entity, model);
            return task ?? (Task<OperationResult>)Task.CompletedTask;
        }

        protected virtual OperationResult DoOnSuccess(TEntity entity)
        {
            var result = OnSuccess?.Invoke(entity);
            return result ?? new OperationResult(0);
        }

        protected virtual OperationResult DoOnError(IFormModel id, string errMessage)
        {
            var result = OnError?.Invoke(id, errMessage);
            return result ?? new OperationResult(-1);
        }

        #endregion

        #region Private Methods

        private int HandleResult(OperationResult result, string methodName)
        {
            if (!result.IsSuccess && !result.NotChanged)
            {
                throw new Exception($"Operation in {methodName} failed", new Exception(result.GetExceptionsString()));
            }
            return result.Rows;
        }

        #endregion
    }
}
