using AutoMapper;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.DataService.Services;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Interfaces.Context;
using Framework.User.DataService.Contract.Models;
using Framework.User.DataService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Services
{
    public class LegalDocumentDbService : BaseEntityService<LegalDocument, LegalDocumentModel>, ILegalDocumentDbService
    {
        public LegalDocumentDbService(ILegalDocumentContext dbContext, IMapper mapper)
            : base(dbContext as IDbContext, mapper, nameof(ConsentDbService))
        {

        }

        public async Task<LegalDocumentModel> Update(LegalDocumentModel model)
        {
            var currentEntity = await _dbContext.GetEntityForUpdate<LegalDocument>(model.Id);

            // TODO бросать исключение? возвращать доп. код ошибки?
            if (currentEntity.IsApproved) return _mapper.Map<LegalDocumentModel>(currentEntity);

            _mapper.Map(model, currentEntity);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<LegalDocumentModel>(currentEntity);
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

        protected void ApplyFilters(ref IQueryable<LegalDocument> list, LegalDocumentFilterModel filter)
        {
            list = list.ApplyFilters(filter);
        }
    }
}
