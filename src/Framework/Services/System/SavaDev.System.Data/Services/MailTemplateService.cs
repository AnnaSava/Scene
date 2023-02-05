using AutoMapper;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Services;
using SavaDev.System.Data.Contract;
using SavaDev.System.Data.Contract.Context;
using SavaDev.System.Data.Contract.Models;

namespace SavaDev.System.Data.Services
{
    public class MailTemplateService : BaseEntityService<Entities.MailTemplate, MailTemplateModel>, IMailTemplateService
    {
        protected DocumentEntityManager<long, Entities.MailTemplate, MailTemplateModel> entityManager;

        public MailTemplateService(SystemContext dbContext, IEnumerable<string> availableCultures, IMapper mapper, ILogger<MailTemplateService> logger)
            : base(dbContext, mapper, nameof(MailTemplateService))
        {
            entityManager = new DocumentEntityManager<long, Entities.MailTemplate, MailTemplateModel>(dbContext, availableCultures, mapper, logger);
        }

        public async Task<OperationResult<MailTemplateModel>> Create(MailTemplateModel model) => await entityManager.Create(model);
        public async Task<OperationResult<MailTemplateModel>> CreateTranslation(MailTemplateModel model) => await entityManager.CreateTranslation(model);
        public async Task<OperationResult<MailTemplateModel>> Update(long id, MailTemplateModel model) => await entityManager.Update(id, model);
        public async Task<OperationResult<MailTemplateModel>> CreateVersion(MailTemplateModel model) => await entityManager.CreateVersion(model);
        public async Task<OperationResult> Publish(long id) => await entityManager.Publish(id);
        public async Task<OperationResult> Delete(long id) => await entityManager.Delete(id);
        public async Task<OperationResult> Restore(long id) => await entityManager.Restore(id);

        public async Task<MailTemplateModel> GetOne(long id) => await entityManager.GetOne<MailTemplateModel>(id);
        public async Task<MailTemplateModel> GetActual(string permName, string culture) => await entityManager.GetActual<MailTemplateModel>(permName, culture);
        public async Task<bool> CheckPermNameExists(string permName) => await entityManager.CheckPermNameExists(permName);
        public async Task<bool> CheckTranslationExists(string permName, string culture) => await entityManager.CheckTranslationExists(permName, culture);
        public async Task<bool> CheckHasAllTranslations(string permName) => await entityManager.CheckHasAllTranslations(permName);
        public async Task<IEnumerable<string>> GetMissingCultures(string permName) => await entityManager.GetMissingCultures(permName);

        //public async Task<PageListModel<MailTemplateModel>> GetAll(ListQueryModel<MailTemplateFilterModel> query)
        //{
        //    return await _dbContext.GetAll<Entities.MailTemplate, MailTemplateModel, MailTemplateFilterModel>(query, ApplyFilters, _mapper);
        //}
        //protected void ApplyFilters(ref IQueryable<Entities.MailTemplate> list, MailTemplateFilterModel filter)
        //{
        //    list = list.ApplyFilters(filter);
        //}
    }
}
