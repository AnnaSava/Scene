using AutoMapper;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.DataService.Services;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Models;
using Framework.User.DataService.Entities;
using Framework.User.DataService.Services.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.User.DataService.Services
{
    public class ReservedNameDbService : AnyEntityService<ReservedName, ReservedNameModel>, IReservedNameDbService
    {
        private readonly IReservedNameContext _context;

        public ReservedNameDbService(IReservedNameContext dbContext, IMapper mapper) 
            : base(dbContext as IDbContext, mapper, nameof(ReservedNameDbService))
        {
            _context = dbContext;
        }

        public async Task<ReservedNameModel> GetOne(string text)
        {
            return await GetOne(m => m.Text == text.ToLower());
        }
       
        public async Task<ReservedNameModel> Update(ReservedNameModel model)
        {
            return await Update(model, m => m.Text == model.Text);
        }

        public async Task Remove(string text)
        {
            await Remove(m => m.Text == text.ToLower());
        }

        public async Task<PageListModel<ReservedNameModel>> GetAll(ListQueryModel<ReservedNameFilterModel> query)
        {
           return await _dbContext.GetAll<ReservedName, ReservedNameModel, ReservedNameFilterModel>(query, ApplyFilters, m => m.Text, _mapper);
        }

        protected void ApplyFilters(ref IQueryable<ReservedName> list, ReservedNameFilterModel filter)
        {
            list = list.ApplyFilters(filter);
        }

        public async Task<bool> CheckIsReserved(string text)
        {
            if (text == null) throw new ArgumentNullException();

            var normalizedText = text.ToLower();
            return await _context.ReservedNames
                  .Select(m => m.Text)
                  .Union(_context.ReservedNames
                  .Where(m => m.IncludePlural)
                  .Select(m => m.Text + "s"))
                  .AnyAsync(m => m == normalizedText);
        }

        public async Task<bool> CheckExists(string text)
        {
            if (text == null) throw new ArgumentNullException();

            return await _context.ReservedNames.AnyAsync(m => m.Text == text.ToLower());
        }
    }
}
