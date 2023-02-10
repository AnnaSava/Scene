using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Services;
using SavaDev.Scheme.Contract.Models;
using SavaDev.Scheme.Data.Contract;
using SavaDev.Scheme.Data.Contract.Models;
using SavaDev.Scheme.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Data.Services
{
    public class FilterService : BaseEntityService<Filter, FilterModel>, IFilterService
    {
        private CreateManager<Filter, FilterModel> CreateManager { get; }
        protected UpdateManager<long, Filter, FilterModel> UpdateManager { get; }
        protected UpdateSelector<long, Filter> UpdateSelector { get; }
        protected RemoveManager<long, Filter> RemoveManager { get; }
        protected OneSelector<Filter> OneSelector { get; }


        #region Constructors

        public FilterService(SchemeContext dbContext, IMapper mapper, ILogger<ColumnService> logger)
            : base(dbContext, mapper, nameof(ColumnService))
        {
            CreateManager = new CreateManager<Filter, FilterModel>(dbContext, mapper, logger);
            UpdateSelector = new UpdateSelector<long, Filter>(dbContext, mapper, logger);
            UpdateManager = new UpdateManager<long, Filter, FilterModel>(dbContext, mapper, logger, UpdateSelector);
            RemoveManager = new RemoveManager<long, Filter>(dbContext, mapper, logger, UpdateSelector);
            OneSelector = new OneSelector<Filter>(dbContext, mapper, logger);
        }

        #endregion

        #region Public Methods: Mutation

        public async Task<OperationResult> Create(FilterModel model)
            => await CreateManager.Create(model);

        public async Task<OperationResult> Update(long id, FilterModel model)
        {
            var result = await UpdateManager.Update(id, model);
            return result;
        }

        public async Task<OperationResult> Remove(long id)
            => await RemoveManager.Remove(id);

        #endregion

        public async Task<IEnumerable<FilterModel>> GetAll(Guid tableId)
        {
            var list = await _dbContext.Set<Filter>()
                .Where(m => m.TableId == tableId)
                .ProjectTo<FilterModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return list;
        }

        public async Task<FilterModel> GetLast(Guid tableId)
        {
            var entity = await _dbContext.Set<Filter>().Where(m => m.TableId == tableId)
                .OrderByDescending(m => m.Id).FirstOrDefaultAsync();
            return _mapper.Map<FilterModel>(entity);
        }

        public async Task<FilterModel> GetOne(long id)
        {
            var model = await OneSelector.GetOne<FilterModel>(m => m.Id == id);
            return model;
        }
    }
}
