using AutoMapper;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.DataService.Services;
using Framework.Base.Types.Enums;
using Framework.MailTemplate.Data.Contract;
using Framework.MailTemplate.Data.Contract.Context;
using Framework.MailTemplate.Data.Contract.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MailTemplate.Data.Services
{
    public class MailTemplateDbService : BaseEntityService<Entities.MailTemplate, MailTemplateModel>, IMailTemplateDbService
    {
        //TODO вынести в настройки проекта appsettings
        private readonly IEnumerable<string> _availableCultures;

        public MailTemplateDbService(IMailTemplateContext dbContext, IEnumerable<string> availableCultures, IMapper mapper)
            : base(dbContext, mapper, nameof(MailTemplateDbService))
        {
            _availableCultures = availableCultures;
        }

        public override async Task<MailTemplateModel> Create(MailTemplateModel model)
        {
            var exists = await CheckPermNameExists(model.PermName);
            if (exists)
            {
                throw new InvalidOperationException($"Template {model.PermName} already exists.");
            }

            return await _dbContext.Create<Entities.MailTemplate, MailTemplateModel>(model, _mapper, OnAdding);
        }

        public async Task<MailTemplateModel> CreateTranslation(MailTemplateModel model)
        {
            var exists = await CheckTranslationExists(model.PermName, model.Culture);
            if (exists)
            {
                throw new InvalidOperationException($"Template {model.PermName} with culture {model.Culture} already exists.");
            }

            return await _dbContext.Create<Entities.MailTemplate, MailTemplateModel>(model, _mapper, OnAdding);
        }

        public async Task<MailTemplateModel> Update(MailTemplateModel model)
        {
            var currentEntity = await _dbContext.GetEntityForUpdate<Entities.MailTemplate>(model.Id);

            // TODO бросать исключение? возвращать доп. код ошибки?
            if (currentEntity.Status != DocumentStatus.Draft)
            {
                throw new Exception($"Template {currentEntity.PermName} Id={currentEntity.Id} is already approved and cannot be updated.");
            }

            _mapper.Map(model, currentEntity);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<MailTemplateModel>(currentEntity);
        }

        public async Task<MailTemplateModel> CreateVersion(MailTemplateModel model)
        {
            var hasDraft = _dbContext.Set<Entities.MailTemplate>()
                .Any(m => m.PermName == model.PermName && m.Culture == model.Culture && m.Status == DocumentStatus.Draft && m.IsDeleted == false);

            if (hasDraft)
            {
                throw new InvalidOperationException($"Draft for template {model.PermName} with culture {model.Culture} already exists.");
            }

            return await _dbContext.Create<Entities.MailTemplate, MailTemplateModel>(model, _mapper, OnAdding);
        }

        public async Task Publish(long id)
        {
            var currentEntity = await _dbContext.GetEntityForUpdate<Entities.MailTemplate>(id);
            if (currentEntity.Status != DocumentStatus.Draft)
            {
                throw new Exception($"Template {currentEntity.PermName} Id={currentEntity.Id} is already {currentEntity.Status.ToString().ToLower()}.");
            }

            currentEntity.Status = DocumentStatus.Published;

            var publishedEntities = await _dbContext.Set<Entities.MailTemplate>()
                .Where(m => m.PermName == currentEntity.PermName && m.Culture == currentEntity.Culture && m.Status == DocumentStatus.Published && m.IsDeleted == false)
                .ToListAsync();

            foreach (var publishedEntity in publishedEntities)
            {
                publishedEntity.Status = DocumentStatus.Outdated;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<MailTemplateModel> Delete(long id)
        {
            return await _dbContext.Delete<Entities.MailTemplate, MailTemplateModel>(id, _mapper);
        }

        public async Task<MailTemplateModel> Restore(long id)
        {
            // TODO отрефакторить, пишется под тест. В частности, два раза вызывается GetEntityForRestore;
            var entity = await _dbContext.GetEntityForRestore<Entities.MailTemplate>(id);

            // TODO я не очень уверена, что это нужно, но пока пусть будет, потому что зачем нам делать лишний запрос к БД на апдейт?
            if (entity.IsDeleted == false)
            {
                throw new InvalidOperationException($"Cannot restore Mail template id={id} name={entity.PermName}. Mail template is not deleted");
            }

            // TODO здесь, возможно, нужно заменять содержимое или удалять более поздний черновик, но пока пусть будет ошибка
            if (entity.Status == DocumentStatus.Draft)
            {
                if (await _dbContext.Set<Entities.MailTemplate>().AnyAsync(m => m.PermName == entity.PermName && m.Status == DocumentStatus.Draft && m.IsDeleted == false))
                {
                    throw new InvalidOperationException($"Cannot restore Mail template id={id} name={entity.PermName} status=Draft. Draft already exists.");
                }
            }

            // TODO здесь, возможно, нужно менять статус на Outdated, но пока пусть будет ошибка
            if (entity.Status == DocumentStatus.Published)
            {
                if (await _dbContext.Set<Entities.MailTemplate>().AnyAsync(m => m.PermName == entity.PermName && m.Status == DocumentStatus.Published && m.IsDeleted == false))
                {
                    throw new InvalidOperationException($"Cannot restore Mail template id={id} name={entity.PermName} status=Published. Published template already exists.");
                }
            }

            return await _dbContext.Restore<Entities.MailTemplate, MailTemplateModel>(id, _mapper);
        }

        public async Task<MailTemplateModel> GetOne(long id)
        {
            return await _dbContext.GetOne<Entities.MailTemplate, MailTemplateModel>(m => m.Id == id, _mapper);
        }

        public async Task<MailTemplateModel> GetActual(string permName, string culture)
        {
            var entity = await _dbContext.Set<Entities.MailTemplate>()
                .Where(m => m.PermName == permName && m.Culture == culture && m.Status == DocumentStatus.Published && m.IsDeleted == false)
                .FirstOrDefaultAsync();

            return _mapper.Map<MailTemplateModel>(entity);
        }

        public async Task<PageListModel<MailTemplateModel>> GetAll(ListQueryModel<MailTemplateFilterModel> query)
        {
            return await _dbContext.GetAll<Entities.MailTemplate, MailTemplateModel, MailTemplateFilterModel>(query, ApplyFilters, _mapper);
        }

        public async Task<bool> CheckPermNameExists(string permName)
        {
            return await _dbContext.Set<Entities.MailTemplate>().AnyAsync(m => m.PermName == permName);
        }

        public async Task<bool> CheckTranslationExists(string permName, string culture)
        {
            return await _dbContext.Set<Entities.MailTemplate>().AnyAsync(m => m.PermName == permName && m.Culture == culture);
        }

        public async Task<bool> CheckHasAllTranslations(string permName)
        {
            var existingCultures = await _dbContext.Set<Entities.MailTemplate>()
                .Where(m => m.PermName == permName)
                .Select(m => m.Culture)
                .Distinct()
                .ToListAsync();

            var intersect = _availableCultures.Intersect(existingCultures);
            return intersect.Count() == _availableCultures.Count();
        }

        public async Task<IEnumerable<string>> GetMissingCultures(string permName)
        {
            var existingCultures = await _dbContext.Set<Entities.MailTemplate>()
                .Where(m => m.PermName == permName)
                .Select(m => m.Culture)
                .Distinct()
                .ToListAsync();

            return _availableCultures.Except(existingCultures);
        }

        protected void ApplyFilters(ref IQueryable<Entities.MailTemplate> list, MailTemplateFilterModel filter)
        {
            list = list.ApplyFilters(filter);
        }

        protected override void OnAdding(Entities.MailTemplate entity)
        {
            base.OnAdding(entity);

            entity.Status = DocumentStatus.Draft;
            entity.Created = entity.LastUpdated = DateTime.Now;
        }
    }
}
