using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Services;
using SavaDev.System.Data.Contract;
using SavaDev.System.Data.Contract.Models;
using SavaDev.System.Data.Entities;

namespace SavaDev.System.Data.Services
{
    public class ReservedNameDbService : BaseEntityService<ReservedName, ReservedNameModel>, IReservedNameDbService
    {
        private readonly AnyEntityManager<ReservedName, ReservedNameModel> entityManager;

        public ReservedNameDbService(IDbContext dbContext, IMapper mapper, ILogger<ReservedName> logger)
            : base(dbContext, mapper, nameof(ReservedNameDbService))
        {
            entityManager = new AnyEntityManager<ReservedName, ReservedNameModel>(dbContext, mapper, logger);
        }

        public async Task<OperationResult<ReservedNameModel>> Create(ReservedNameModel model) => await entityManager.Create(model);
        public async Task<OperationResult<ReservedNameModel>> Update(ReservedNameModel model) => await entityManager.Update(m => m.Text == model.Text, model);
        public async Task<OperationResult> Remove(string text) => await entityManager.Remove(m => m.Text == text.ToLower());

        public async Task<ReservedNameModel> GetOne(string text) => await entityManager.GetOne<ReservedNameModel>(m => m.Text == text.ToLower());
        public async Task<bool> CheckIsReserved(string text)
        {
            if (text == null) throw new ArgumentNullException();

            var normalizedText = text.ToLower();
            return await _dbContext.Set<ReservedName>()
                  .Select(m => m.Text)
                  .Union(_dbContext.Set<ReservedName>()
                  .Where(m => m.IncludePlural)
                  .Select(m => m.Text + "s"))
                  .AnyAsync(m => m == normalizedText);
        }

        public async Task<bool> CheckExists(string text)
        {
            if (text == null) throw new ArgumentNullException();

            return await entityManager.CheckExists(m => m.Text == text.ToLower());
        }

        //public async Task<PageListModel<ReservedNameModel>> GetAll(ListQueryModel<ReservedNameFilterModel> query)
        //{
        //    return await _dbContext.GetAll<ReservedName, ReservedNameModel, ReservedNameFilterModel>(query, ApplyFilters, m => m.Text, _mapper);
        //}

        //protected void ApplyFilters(ref IQueryable<ReservedName> list, ReservedNameFilterModel filter)
        //{
        //    list = list.ApplyFilters(filter);
        //}
    }
}
