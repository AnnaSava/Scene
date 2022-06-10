using AutoMapper;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.DataService.Services;
using Framework.Base.Types.Enums;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Interfaces.Context;
using Framework.User.DataService.Contract.Models;
using Framework.User.DataService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Services
{
    public class LegalDocumentDbService : BaseEntityService<LegalDocument, LegalDocumentModel>, ILegalDocumentDbService
    {
        //TODO вынести в настройки проекта appsettings
        private readonly IEnumerable<string> _availableCultures;

        private readonly ILegalDocumentContext _legalDocumentContext;

        public LegalDocumentDbService(ILegalDocumentContext dbContext, IEnumerable<string> availableCultures, IMapper mapper)
            : base(dbContext as IDbContext, mapper, nameof(LegalDocumentDbService))
        {
            _legalDocumentContext = dbContext;
            _availableCultures = availableCultures;
        }

        public override async Task<LegalDocumentModel> Create(LegalDocumentModel model)
        {
            var exists = await CheckDocumentExists(model.PermName);
            if(exists)
            {
                throw new InvalidOperationException($"Document {model.PermName} already exists.");
            }

            return await _dbContext.Create<LegalDocument, LegalDocumentModel>(model, _mapper, OnAdding);
        }

        public async Task<LegalDocumentModel> CreateTranslation(LegalDocumentModel model)
        {
            var exists = await CheckTranslationExists(model.PermName, model.Culture);
            if (exists)
            {
                throw new InvalidOperationException($"Document {model.PermName} with culture {model.Culture} already exists.");
            }

            return await _dbContext.Create<LegalDocument, LegalDocumentModel>(model, _mapper, OnAdding);
        }

        public async Task<LegalDocumentModel> Update(LegalDocumentModel model)
        {
            var currentEntity = await _dbContext.GetEntityForUpdate<LegalDocument>(model.Id);

            // TODO бросать исключение? возвращать доп. код ошибки?
            if (currentEntity.Status != DocumentStatus.Draft)
            {
                throw new Exception($"Document {currentEntity.PermName} Id={currentEntity.Id} is already approved and cannot be updated.");
            }

            _mapper.Map(model, currentEntity);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<LegalDocumentModel>(currentEntity);
        }

        public async Task<LegalDocumentModel> CreateVersion(LegalDocumentModel model)
        {
            var hasDraft = _dbContext.Set<LegalDocument>()
                .Any(m => m.PermName == model.PermName && m.Culture == model.Culture && m.Status == DocumentStatus.Draft && m.IsDeleted == false);

            if (hasDraft)
            {
                throw new InvalidOperationException($"Draft for document {model.PermName} with culture {model.Culture} already exists.");
            }

            return await _dbContext.Create<LegalDocument, LegalDocumentModel>(model, _mapper, OnAdding);
        }

        public async Task Publish(long id)
        {
            var currentEntity = await _dbContext.GetEntityForUpdate<LegalDocument>(id);
            if (currentEntity.Status != DocumentStatus.Draft)
            {
                throw new Exception($"Document {currentEntity.PermName} Id={currentEntity.Id} is already {currentEntity.Status.ToString().ToLower()}.");
            }

            currentEntity.Status = DocumentStatus.Published;

            var publishedEntities = await _dbContext.Set<LegalDocument>()
                .Where(m => m.PermName == currentEntity.PermName && m.Culture == currentEntity.Culture && m.Status == DocumentStatus.Published && m.IsDeleted == false)
                .ToListAsync();

            foreach (var publishedEntity in publishedEntities)
            {
                publishedEntity.Status = DocumentStatus.Outdated;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<LegalDocumentModel> Delete(long id)
        {
            return await _dbContext.Delete<LegalDocument, LegalDocumentModel>(id, _mapper);
        }

        public async Task<LegalDocumentModel> Restore(long id)
        {
            // TODO отрефакторить, пишется под тест. В частности, два раза вызывается GetEntityForRestore;
            var entity = await _dbContext.GetEntityForRestore<LegalDocument>(id);
            
            // TODO я не очень уверена, что это нужно, но пока пусть будет, потому что зачем нам делать лишний запрос к БД на апдейт?
            if(entity.IsDeleted == false)
            {
                throw new InvalidOperationException($"Cannot restore Legal document id={id} name={entity.PermName}. Legal document is not deleted");
            }

            // TODO здесь, возможно, нужно заменять содержимое или удалять более поздний черновик, но пока пусть будет ошибка
            if (entity.Status == DocumentStatus.Draft)
            {
                if (await _dbContext.Set<LegalDocument>().AnyAsync(m => m.PermName == entity.PermName && m.Status == DocumentStatus.Draft && m.IsDeleted == false))
                {
                    throw new InvalidOperationException($"Cannot restore Legal document id={id} name={entity.PermName} status=Draft. Draft already exists.");
                }
            }

            // TODO здесь, возможно, нужно менять статус на Outdated, но пока пусть будет ошибка
            if (entity.Status == DocumentStatus.Published)
            {
                if (await _dbContext.Set<LegalDocument>().AnyAsync(m => m.PermName == entity.PermName && m.Status == DocumentStatus.Published && m.IsDeleted == false))
                {
                    throw new InvalidOperationException($"Cannot restore Legal document id={id} name={entity.PermName} status=Published. Published document already exists.");
                }
            }

            return await _dbContext.Restore<LegalDocument, LegalDocumentModel>(id, _mapper);
        }

        public async Task<LegalDocumentModel> GetOne(long id)
        {
            return await _dbContext.GetOne<LegalDocument, LegalDocumentModel>(m => m.Id == id, _mapper);
        }

        public async Task<LegalDocumentModel> GetActual(string permName, string culture)
        {
            var entity = await _dbContext.Set<LegalDocument>()
                .Where(m => m.PermName == permName && m.Culture == culture && m.Status == DocumentStatus.Published && m.IsDeleted == false)
                .FirstOrDefaultAsync();

            return _mapper.Map<LegalDocumentModel>(entity);
        }

        public async Task<PageListModel<LegalDocumentModel>> GetAll(ListQueryModel<LegalDocumentFilterModel> query)
        {
            return await _dbContext.GetAll<LegalDocument, LegalDocumentModel, LegalDocumentFilterModel>(query, ApplyFilters, _mapper);
        }

        public async Task<bool> CheckDocumentExists(string permName)
        {
            return await _legalDocumentContext.LegalDocuments.AnyAsync(m => m.PermName == permName);
        }

        public async Task<bool> CheckTranslationExists(string permName, string culture)
        {
            return await _legalDocumentContext.LegalDocuments.AnyAsync(m => m.PermName == permName && m.Culture == culture);
        }

        public async Task<bool> CheckHasAllTranslations(string permName)
        {
            var existingCultures = await _dbContext.Set<LegalDocument>()
                .Where(m => m.PermName == permName)
                .Select(m => m.Culture)
                .Distinct()
                .ToListAsync();

            var intersect = _availableCultures.Intersect(existingCultures);
            return intersect.Count() == _availableCultures.Count();
        }

        public async Task<IEnumerable<string>> GetMissingCultures(string permName)
        {
            var existingCultures = await _dbContext.Set<LegalDocument>()
                .Where(m => m.PermName == permName)
                .Select(m => m.Culture)
                .Distinct()
                .ToListAsync();

            return _availableCultures.Except(existingCultures);
        }

        protected void ApplyFilters(ref IQueryable<LegalDocument> list, LegalDocumentFilterModel filter)
        {
            list = list.ApplyFilters(filter);
        }

        protected override void OnAdding(LegalDocument entity)
        {
            base.OnAdding(entity);

            entity.Status = DocumentStatus.Draft;
            entity.Created = entity.LastUpdated = DateTime.Now;
        }
    }
}
