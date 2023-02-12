using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Entities;
using SavaDev.Base.Data.Enums;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Managers.Crud;
using SavaDev.Base.Data.Models;
using SavaDev.Base.Data.Models.Interfaces;

namespace SavaDev.Base.Data.Services
{
    public class BaseDocumentEntityService<TEntity, TFormModel> : BaseDocumentEntityService<long, TEntity, TFormModel>
        where TEntity : BaseDocumentEntity<long>, new()
        where TFormModel : BaseDocumentFormModel<long>, IAnyModel
    {
        public BaseDocumentEntityService(IDbContext dbContext, 
            IEnumerable<string> availableCultures, 
            IMapper mapper, 
            ILogger logger, 
            string serviceName)
            : base(dbContext, availableCultures, mapper, logger)
        {

        }
    }

    public class BaseDocumentEntityService<TKey, TEntity, TFormModel> : BaseEntityService<TEntity, TFormModel>
        where TEntity : BaseDocumentEntity<TKey>, new()
        where TFormModel : BaseDocumentFormModel<TKey>, IAnyModel
    {
        #region Private Fields: Dependencies

        private readonly ILogger _logger;
        private readonly IEnumerable<string> _availableCultures;

        #endregion

        #region Protected Properties : Managers

        protected CreateManager<TEntity, TFormModel> CreateManager { get; }
        protected UpdateRestorableSelector<TKey, TEntity> UpdateSelector { get; }
        protected UpdateManager<TKey, TEntity, TFormModel> UpdateManager { get; }
        protected UpdateFieldManager<TKey, TEntity> FieldSetterManager { get; }
        protected RestoreSelector<TKey, TEntity> RestoreSelector { get; }
        protected UpdateFieldManager<TKey, TEntity> RestoreManager { get; }
        protected OneRestorableSelector<TKey, TEntity> OneSelector { get; }

        #endregion

        #region Public Constructors

        public BaseDocumentEntityService(IDbContext dbContext, IEnumerable<string> availableCultures, IMapper mapper, ILogger logger)
            : base(dbContext, mapper, "")
        {
            _logger = logger;

            _availableCultures = availableCultures;

            CreateManager = new CreateManager<TEntity, TFormModel>(dbContext, mapper, logger);
            UpdateSelector = new UpdateRestorableSelector<TKey, TEntity>(dbContext, mapper, logger);
            UpdateManager = new UpdateManager<TKey, TEntity, TFormModel>(dbContext, mapper, logger, UpdateSelector);
            FieldSetterManager = new UpdateFieldManager<TKey, TEntity>(dbContext, mapper, logger, UpdateSelector);
            RestoreSelector = new RestoreSelector<TKey, TEntity>(dbContext, mapper, logger);
            OneSelector = new OneRestorableSelector<TKey, TEntity>(dbContext, mapper, logger);
            RestoreManager = new UpdateFieldManager<TKey, TEntity>(dbContext, mapper, logger, RestoreSelector);
        }

        #endregion

        #region Public Methods: Mutation

        public async Task<OperationResult> Create(TFormModel model)
        {
            var result = await CreateManager.Create(model, validate: Validate, setValues: SetValuesCreating);
            return result;
        }

        public async Task<OperationResult> CreateTranslation(TFormModel model)
        {
            var result = await CreateManager.Create(model, validate: ValidateTranslation, setValues: SetValuesCreating);

            return result;
        }

        public async Task<OperationResult> Update(TKey id, TFormModel model)
        {
            var result = await UpdateManager.Update(id, model, async (entity) =>
            {
                if (entity.Status != DocumentStatus.Draft)
                {
                    throw new Exception($"Document {entity.PermName} Id={entity.Id} is already approved and cannot be updated.");
                }
            });

            return result;
        }

        public async Task<OperationResult> CreateVersion(TFormModel model)
        {
            var result = await CreateManager.Create(model, validate: ValidateVersion, setValues: SetValuesCreating);

            return result;
        }

        public async Task<OperationResult> Publish(TKey id)
        {
            var updater = new EntityUpdater<TKey, TEntity>(_dbContext, _logger)
                .GetEntity(async (id) => await UpdateSelector.GetEntityForUpdate(id))
                .ValidateEntity(async (entity) =>
                {
                    if (entity.Status != DocumentStatus.Draft)
                    {
                        throw new Exception($"Template {entity.PermName} Id={entity.Id} is already {entity.Status.ToString().ToLower()}.");
                    }
                })
                .SetValues(async (entity) => entity.Status = DocumentStatus.Published)
                .Update(DoUpdate)
                .AfterUpdate(async (currentEntity) =>
                {
                    var publishedEntities = await _dbContext.Set<TEntity>()
                    .Where(m => m.PermName == currentEntity.PermName 
                        && m.Culture == currentEntity.Culture 
                        && m.Status == DocumentStatus.Published 
                        && m.IsDeleted == false
                        && !m.Id.Equals(currentEntity.Id))
                    .ToListAsync();

                    foreach (var publishedEntity in publishedEntities)
                    {
                        publishedEntity.Status = DocumentStatus.Outdated;
                    }
                    // TODO как-то перенести внутрь апдейтера
                    var rows = await _dbContext.SaveChangesAsync();
                    return new OperationResult(rows);

                })
            .SuccessResult(entity => new OperationResult(1, id))
            .ErrorResult((id, errMessage) => new OperationResult(DbOperationRows.OnFailure, id, new OperationExceptionInfo(errMessage)));

            return await updater.DoUpdate(id);
        }

        public async Task<OperationResult> Delete(TKey id)
        {
            var result = await FieldSetterManager.SetField(id, entity => entity.IsDeleted = true);
            return result;
        }

        public async Task<OperationResult> Restore(TKey id)
        {
            var result = await RestoreManager.SetField(id, entity => entity.IsDeleted = false, async (entity) =>
            {
                // TODO я не очень уверена, что это нужно, но пока пусть будет, потому что зачем нам делать лишний запрос к БД на апдейт?
                if (entity.IsDeleted == false)
                {
                    throw new InvalidOperationException($"Cannot restore Legal document id={entity.Id} name={entity.PermName}. Legal document is not deleted");
                }

                // TODO здесь, возможно, нужно заменять содержимое или удалять более поздний черновик, но пока пусть будет ошибка
                if (entity.Status == DocumentStatus.Draft)
                {
                    if (await _dbContext.Set<TEntity>().AnyAsync(m => m.PermName == entity.PermName && m.Status == DocumentStatus.Draft && m.IsDeleted == false))
                    {
                        throw new InvalidOperationException($"Cannot restore Legal document id={entity.Id} name={entity.PermName} status=Draft. Draft already exists.");
                    }
                }

                // TODO здесь, возможно, нужно менять статус на Outdated, но пока пусть будет ошибка
                if (entity.Status == DocumentStatus.Published)
                {
                    if (await _dbContext.Set<TEntity>().AnyAsync(m => m.PermName == entity.PermName && m.Status == DocumentStatus.Published && m.IsDeleted == false))
                    {
                        throw new InvalidOperationException($"Cannot restore Legal document id={entity.Id} name={entity.PermName} status=Published. Published document already exists.");
                    }
                }
            });
            return result;
        }


        #endregion


        #region Public Methods: Query

        public async Task<TModel> GetOne<TModel>(TKey id) => await OneSelector.GetOne<TModel>(id);

        public async Task<TModel> GetActual<TModel>(string permName, string culture)
        {
            var model = await OneSelector.GetOne<TFormModel>(m => m.PermName == permName && m.Culture == culture && m.Status == DocumentStatus.Published && m.IsDeleted == false);
            return _mapper.Map<TModel>(model);
        }

        public async Task<bool> CheckPermNameExists(string permName)
        {
            return await _dbContext.Set<TEntity>().AnyAsync(m => m.PermName == permName);
        }

        public async Task<bool> CheckTranslationExists(string permName, string culture)
        {
            return await _dbContext.Set<TEntity>().AnyAsync(m => m.PermName == permName && m.Culture == culture);
        }

        public async Task<bool> CheckHasAllTranslations(string permName)
        {
            var existingCultures = await _dbContext.Set<TEntity>()
                .Where(m => m.PermName == permName)
                .Select(m => m.Culture)
                .Distinct()
                .ToListAsync();

            var intersect = _availableCultures.Intersect(existingCultures);
            return intersect.Count() == _availableCultures.Count();
        }

        public async Task<IEnumerable<string>> GetMissingCultures(string permName)
        {
            var existingCultures = await _dbContext.Set<TEntity>()
                .Where(m => m.PermName == permName)
                .Select(m => m.Culture)
                .Distinct()
                .ToListAsync();

            return _availableCultures.Except(existingCultures);
        }

        #endregion

        #region Private Methods

        private async Task Validate(IFormModel model)
        {
            var docModel = model as BaseDocumentFormModel<long>;
            if (docModel?.PermName == null)
            {
                throw new ArgumentNullException("PermName is null.");
            }
            var exists = await CheckPermNameExists(docModel.PermName);
            if (exists)
            {
                throw new InvalidOperationException($"Document {docModel.PermName} already exists.");
            }
        }

        private async Task ValidateTranslation(IFormModel model)
        {
            var docModel = model as BaseDocumentFormModel<long>;
            if (docModel?.PermName == null)
            {
                throw new ArgumentNullException("PermName is null.");
            }
            var exists = await CheckTranslationExists(docModel.PermName, docModel.Culture);
            if (exists)
            {
                throw new InvalidOperationException($"Document {docModel.PermName} with culture {docModel.Culture} already exists.");
            }
        }

        private async Task ValidateVersion(IFormModel model)
        {
            var docModel = model as BaseDocumentFormModel<long>;
            if (docModel?.PermName == null)
            {
                throw new ArgumentNullException("PermName is null.");
            }
            var hasDraft = await _dbContext.Set<TEntity>()
                .AnyAsync(m => m.PermName == docModel.PermName && m.Culture == docModel.Culture
                && m.Status == DocumentStatus.Draft && m.IsDeleted == false);

            if (hasDraft)
            {
                throw new InvalidOperationException($"Draft for document {docModel.PermName} with culture {docModel.Culture} already exists.");
            }
        }

        private void SetValuesCreating(TEntity entity)
        {
            entity.Status = DocumentStatus.Draft;
            entity.Created = entity.LastUpdated = DateTime.UtcNow;
        }

        // TODO подумать, как объединить с такими же методами в манагере
        private async Task<OperationResult> DoUpdate(TEntity entity)
        {
            var rows = await _dbContext.SaveChangesAsync();
            return new OperationResult(rows);
        }

        #endregion
    }
}
