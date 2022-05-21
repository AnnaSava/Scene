using AutoMapper;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.DataService.Services;
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
        private readonly ILegalDocumentContext _legalDocumentContext;

        public LegalDocumentDbService(ILegalDocumentContext dbContext, IMapper mapper)
            : base(dbContext as IDbContext, mapper, nameof(LegalDocumentDbService))
        {
            _legalDocumentContext = dbContext;
        }

        public override async Task<LegalDocumentModel> Create(LegalDocumentModel model)
        {
            var exists = await CheckDocumentExisis(model.PermName);
            if(exists)
            {
                throw new Exception($"Document {model.PermName} already exists.");
            }

            return await _dbContext.Create<LegalDocument, LegalDocumentModel>(model, _mapper, OnAdding);
        }

        public async Task<LegalDocumentModel> CreateTranslation(LegalDocumentModel model)
        {
            var exists = await CheckTranslationExisis(model.PermName, model.Culture);
            if (exists)
            {
                throw new Exception($"Document {model.PermName} with culture {model.Culture} already exists.");
            }

            return await _dbContext.Create<LegalDocument, LegalDocumentModel>(model, _mapper, OnAdding);
        }

        public async Task<LegalDocumentModel> Update(LegalDocumentModel model)
        {
            var currentEntity = await _dbContext.GetEntityForUpdate<LegalDocument>(model.Id);

            // TODO бросать исключение? возвращать доп. код ошибки?
            if (currentEntity.Status != Base.Types.Enums.DocumentStatus.Draft)
            {
                //return _mapper.Map<LegalDocumentModel>(currentEntity);
                throw new Exception($"Document {model.PermName} Id={model.Id} is already approved and cannot be updated.");
            }

            _mapper.Map(model, currentEntity);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<LegalDocumentModel>(currentEntity);
        }

        public async Task<LegalDocumentModel> CreateVersion(LegalDocumentModel model)
        {
            return await _dbContext.Create<LegalDocument, LegalDocumentModel>(model, _mapper, OnAdding);
        }

        public async Task Approve(long id)
        {
            var currentEntity = await _dbContext.GetEntityForUpdate<LegalDocument>(id);
            currentEntity.Status = Base.Types.Enums.DocumentStatus.Published;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<LegalDocumentModel> Delete(long id)
        {
            return await _dbContext.Delete<LegalDocument, LegalDocumentModel>(id, _mapper);
        }

        public async Task<LegalDocumentModel> Restore(long id)
        {
            return await _dbContext.Restore<LegalDocument, LegalDocumentModel>(id, _mapper);
        }

        public async Task<LegalDocumentModel> GetOne(long id)
        {
            return await _dbContext.GetOne<LegalDocument, LegalDocumentModel>(m => m.Id == id, _mapper);
        }

        public async Task<PageListModel<LegalDocumentModel>> GetAll(ListQueryModel<LegalDocumentFilterModel> query)
        {
            return await _dbContext.GetAll<LegalDocument, LegalDocumentModel, LegalDocumentFilterModel>(query, ApplyFilters, _mapper);
        }

        public async Task<bool> CheckDocumentExisis(string permName)
        {
            return await _legalDocumentContext.LegalDocuments.AnyAsync(m => m.PermName == permName);
        }

        public async Task<bool> CheckTranslationExisis(string permName, string culture)
        {
            return await _legalDocumentContext.LegalDocuments.AnyAsync(m => m.PermName == permName && m.Culture == culture);
        }

        protected void ApplyFilters(ref IQueryable<LegalDocument> list, LegalDocumentFilterModel filter)
        {
            list = list.ApplyFilters(filter);
        }
    }
}
