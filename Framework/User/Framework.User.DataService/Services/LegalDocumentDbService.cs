using AutoMapper;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.DataService.Entities;
using Framework.Base.DataService.Services;
using Framework.Base.DataService.Services.Managers;
using Framework.Base.Types.Enums;
using Framework.Base.Types.View;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Interfaces.Context;
using Framework.User.DataService.Contract.Models;
using Framework.User.DataService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Services
{
    public class LegalDocumentDbService : BaseEntityService<LegalDocument, LegalDocumentModel>, ILegalDocumentDbService
    {
        private readonly ILegalDocumentContext _legalDocumentContext;

        protected DocumentEntityManager<long, LegalDocument, LegalDocumentModel> entityManager;

        public LegalDocumentDbService(IDbContext dbContext, IEnumerable<string> availableCultures, IMapper mapper, ILogger<LegalDocumentDbService> logger)
            : base(dbContext, mapper, nameof(LegalDocumentDbService))
        {
            entityManager = new DocumentEntityManager<long, LegalDocument, LegalDocumentModel>(dbContext, availableCultures, mapper, logger);
        }

        public async Task<OperationResult<LegalDocumentModel>> Create(LegalDocumentModel model) => await entityManager.Create(model);
        public async Task<OperationResult<LegalDocumentModel>> CreateTranslation(LegalDocumentModel model) => await entityManager.CreateTranslation(model);
        public async Task<OperationResult<LegalDocumentModel>> Update(long id, LegalDocumentModel model) => await entityManager.Update(id, model);
        public async Task<OperationResult<LegalDocumentModel>> CreateVersion(LegalDocumentModel model) => await entityManager.CreateVersion(model);
        public async Task<OperationResult> Publish(long id) => await entityManager.Publish(id);
        public async Task<OperationResult> Delete(long id) => await entityManager.Delete(id);
        public async Task<OperationResult> Restore(long id) => await entityManager.Restore(id);

        public async Task<LegalDocumentModel> GetOne(long id) => await entityManager.GetOne<LegalDocumentModel>(id);
        public async Task<LegalDocumentModel> GetActual(string permName, string culture) => await entityManager.GetActual<LegalDocumentModel>(permName, culture);
        public async Task<bool> CheckPermNameExists(string permName) => await entityManager.CheckPermNameExists(permName);
        public async Task<bool> CheckTranslationExists(string permName, string culture) => await entityManager.CheckTranslationExists(permName, culture);
        public async Task<bool> CheckHasAllTranslations(string permName) => await entityManager.CheckHasAllTranslations(permName);
        public async Task<IEnumerable<string>> GetMissingCultures(string permName) => await entityManager.GetMissingCultures(permName);

        public async Task<PageListModel<LegalDocumentModel>> GetAll(ListQueryModel<LegalDocumentFilterModel> query)
        {
            return await _dbContext.GetAll<LegalDocument, LegalDocumentModel, LegalDocumentFilterModel>(query, ApplyFilters, _mapper);
        }
        protected void ApplyFilters(ref IQueryable<LegalDocument> list, LegalDocumentFilterModel filter)
        {
            list = list.ApplyFilters(filter);
        }
    }
}
