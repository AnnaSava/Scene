using AutoMapper;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.DataService.Services;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Models;
using Framework.User.DataService.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.User.DataService.Services
{
    public class ConsentDbService : BaseEntityService<Consent, ConsentModel>, IConsentDbService
    {
        public ConsentDbService(IConsentContext dbContext, IMapper mapper)
            : base(dbContext as IDbContext, mapper, nameof(ConsentDbService))
        {

        }

        public async Task<ConsentModel> Update(ConsentModel model)
        {
            var currentEntity = await _dbContext.GetEntityForUpdate<Consent>(model.Id);

            // TODO бросать исключение? возвращать доп. код ошибки?
            if (currentEntity.IsApproved) return _mapper.Map<ConsentModel>(currentEntity);

            _mapper.Map(model, currentEntity);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ConsentModel>(currentEntity);
        }

        public async Task<ConsentModel> Delete(int id)
        {
            return await _dbContext.Delete<Consent, ConsentModel>(id, _mapper);
        }

        public async Task<ConsentModel> Restore(int id)
        {
            return await _dbContext.Restore<Consent, ConsentModel>(id, _mapper);
        }

        public async Task<ConsentModel> GetOne(int id)
        {
            return await _dbContext.GetOne<Consent, ConsentModel>(m => m.Id == id, _mapper);
        }

        public async Task<ConsentModel> GetActual()
        {
            var entity = await _dbContext.Set<Consent>()
                .Where(m => m.IsApproved && m.IsDeleted == false)
                .OrderByDescending(m => m.Created)
                .FirstOrDefaultAsync();

            return _mapper.Map<ConsentModel>(entity);
        }

        public async Task<bool> IsActual(int id)
        {
            var entity = await _dbContext.Set<Consent>()
                .Where(m => m.IsApproved && m.IsDeleted == false)
                .OrderByDescending(m => m.Created)
                .FirstOrDefaultAsync();

            if (entity != null) return entity.Id == id;
            return false;
        }

        public async Task<bool> IsLast(int id)
        {
            var entity = await _dbContext.Set<Consent>()
                .Where(m => m.IsDeleted == false)
                .OrderByDescending(m => m.Created)
                .FirstOrDefaultAsync();

            if (entity != null) return entity.Id == id;
            return false;
        }

        public async Task<PageListModel<ConsentModel>> GetAll(ListQueryModel<ConsentFilterModel> query)
        {
            return await _dbContext.GetAllOrderByInt<Consent, ConsentModel, ConsentFilterModel>(query, ApplyFilters, m => m.Id, _mapper);
        }

        public async Task<bool> AnyConsentExists()
        {
            return await _dbContext.Set<Consent>().AnyAsync();
        }

        protected void ApplyFilters(ref IQueryable<Consent> list, ConsentFilterModel filter)
        {
            list = list.ApplyFilters(filter);
        }
    }
}
