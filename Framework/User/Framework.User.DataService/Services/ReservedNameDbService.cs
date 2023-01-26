using AutoMapper;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.DataService.Services;
using Framework.Base.DataService.Services.Managers;
using Framework.Base.Types.View;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Models;
using Framework.User.DataService.Entities;
using Framework.User.DataService.Services.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.User.DataService.Services
{
    public class ReservedNameDbService : AnyEntityService<ReservedName, ReservedNameModel>, IReservedNameDbService
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

        public async Task<PageListModel<ReservedNameModel>> GetAll(ListQueryModel<ReservedNameFilterModel> query)
        {
            return await _dbContext.GetAll<ReservedName, ReservedNameModel, ReservedNameFilterModel>(query, ApplyFilters, m => m.Text, _mapper);
        }

        protected void ApplyFilters(ref IQueryable<ReservedName> list, ReservedNameFilterModel filter)
        {
            list = list.ApplyFilters(filter);
        }
    }
}
