using AutoMapper;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.Types.Enums;
using Framework.Base.Types.Registry;

namespace Framework.Base.DataService.Services.Managers
{
    [Obsolete]
    public class DocumentEntityManager<TKey, TEntity, TFormModel>
        where TEntity : BaseDocumentEntity<TKey>, new()
        where TFormModel : BaseDocumentFormModel<TKey>, new()
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IEnumerable<string> _availableCultures;

        protected EditableRestorableEntityManager<TKey, TEntity, TFormModel> entityManager;

        public DocumentEntityManager(IDbContext dbContext, IEnumerable<string> availableCultures, IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;

            _availableCultures = availableCultures;
            entityManager = new EditableRestorableEntityManager<TKey, TEntity, TFormModel>(dbContext, mapper, logger);
        }

        protected Action<TEntity> OnCreating = (entity) =>
        {
            entity.Status = DocumentStatus.Draft;
            entity.Created = entity.LastUpdated = DateTime.Now;
        };

        public async Task<OperationResult<TFormModel>> Create(TFormModel model)
        {
            // TODO поставить уникальный ключ в БД, чтобы ошибка генерировалась при попытке сохранения
            var exists = await CheckPermNameExists(model.PermName);
            if (exists)
            {
                throw new InvalidOperationException($"Document {model.PermName} already exists.");
            }

            var result = await entityManager.Create(model, OnCreating);
            return result;
        }

        public async Task<OperationResult<TFormModel>> CreateTranslation(TFormModel model)
        {
            var exists = await CheckTranslationExists(model.PermName, model.Culture);
            if (exists)
            {
                throw new InvalidOperationException($"Document {model.PermName} with culture {model.Culture} already exists.");
            }

            var result = await entityManager.Create(model, OnCreating);
            return result;
        }

        public async Task<OperationResult<TFormModel>> Update(TKey id, TFormModel model)
        {
            var currentEntity = await entityManager.GetEntityForUpdate(id);

            // TODO бросать исключение? возвращать доп. код ошибки?
            if (currentEntity.Status != DocumentStatus.Draft)
            {
                throw new Exception($"Document {currentEntity.PermName} Id={currentEntity.Id} is already approved and cannot be updated.");
            }

            return await entityManager.Update(id, model);
        }

        public async Task<OperationResult<TFormModel>> CreateVersion(TFormModel model)
        {
            var hasDraft = _dbContext.Set<TEntity>()
                .Any(m => m.PermName == model.PermName && m.Culture == model.Culture && m.Status == DocumentStatus.Draft && m.IsDeleted == false);

            if (hasDraft)
            {
                throw new InvalidOperationException($"Draft for document {model.PermName} with culture {model.Culture} already exists.");
            }

            return await entityManager.Create(model);
        }

        public async Task<OperationResult> Publish(TKey id)
        {
            var currentEntity = await entityManager.GetEntityForUpdate(id);
            if (currentEntity.Status != DocumentStatus.Draft)
            {
                throw new Exception($"Template {currentEntity.PermName} Id={currentEntity.Id} is already {currentEntity.Status.ToString().ToLower()}.");
            }

            currentEntity.Status = DocumentStatus.Published;

            var publishedEntities = await _dbContext.Set<TEntity>()
                .Where(m => m.PermName == currentEntity.PermName && m.Culture == currentEntity.Culture && m.Status == DocumentStatus.Published && m.IsDeleted == false)
                .ToListAsync();

            foreach (var publishedEntity in publishedEntities)
            {
                publishedEntity.Status = DocumentStatus.Outdated;
            }

            var rows = await _dbContext.SaveChangesAsync();
            return new OperationResult(rows);
        }

        public async Task<OperationResult> Delete(TKey id) => await entityManager.Delete(id);

        public async Task<OperationResult> Restore(TKey id)
        {
            // TODO отрефакторить, пишется под тест. В частности, два раза вызывается GetEntityForRestore;
            var entity = await entityManager.GetEntityForRestore(id);

            // TODO я не очень уверена, что это нужно, но пока пусть будет, потому что зачем нам делать лишний запрос к БД на апдейт?
            if (entity.IsDeleted == false)
            {
                throw new InvalidOperationException($"Cannot restore Legal document id={id} name={entity.PermName}. Legal document is not deleted");
            }

            // TODO здесь, возможно, нужно заменять содержимое или удалять более поздний черновик, но пока пусть будет ошибка
            if (entity.Status == DocumentStatus.Draft)
            {
                if (await _dbContext.Set<TEntity>().AnyAsync(m => m.PermName == entity.PermName && m.Status == DocumentStatus.Draft && m.IsDeleted == false))
                {
                    throw new InvalidOperationException($"Cannot restore Legal document id={id} name={entity.PermName} status=Draft. Draft already exists.");
                }
            }

            // TODO здесь, возможно, нужно менять статус на Outdated, но пока пусть будет ошибка
            if (entity.Status == DocumentStatus.Published)
            {
                if (await _dbContext.Set<TEntity>().AnyAsync(m => m.PermName == entity.PermName && m.Status == DocumentStatus.Published && m.IsDeleted == false))
                {
                    throw new InvalidOperationException($"Cannot restore Legal document id={id} name={entity.PermName} status=Published. Published document already exists.");
                }
            }

            return await entityManager.Restore(id);
        }

        public async Task<TModel> GetOne<TModel>(TKey id) => await entityManager.GetOne<TModel>(id);

        public async Task<TModel> GetActual<TModel>(string permName, string culture)
        {
            var entity = await _dbContext.Set<TEntity>()
                .Where(m => m.PermName == permName && m.Culture == culture && m.Status == DocumentStatus.Published && m.IsDeleted == false)
                .FirstOrDefaultAsync();

            return _mapper.Map<TModel>(entity);
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
    }
}
