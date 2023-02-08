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
        protected UpdateSelector<long, Filter> UpdateSelector { get; }
        protected RemoveManager<long, Filter> RemoveManager { get; }


        #region Constructors

        public FilterService(SchemeContext dbContext, IMapper mapper, ILogger<ColumnService> logger)
            : base(dbContext, mapper, nameof(ColumnService))
        {
            CreateManager = new CreateManager<Filter, FilterModel>(dbContext, mapper, logger);
            UpdateSelector = new UpdateSelector<long, Filter>(dbContext, mapper, logger);
            RemoveManager = new RemoveManager<long, Filter>(dbContext, mapper, logger, UpdateSelector);
        }

        #endregion

        #region Public Methods: Mutation

        public async Task<OperationResult> Create(FilterModel model)
            => await CreateManager.Create(model);

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
    }
}
