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
        private readonly UpdateManager<long, Filter, FilterModel> updaterManager;
        private readonly UpdateSelector<long, Filter> updateSelectorManager;

        #region Constructors

        public FilterService(SchemeContext dbContext, IMapper mapper, ILogger<ColumnService> logger)
            : base(dbContext, mapper, nameof(ColumnService))
        {
            CreateManager = new CreateManager<Filter, FilterModel>(dbContext, mapper, logger);
            updateSelectorManager = new UpdateSelector<long, Filter>(dbContext, mapper, logger);
            updaterManager = new UpdateManager<long, Filter, FilterModel>(dbContext, mapper, logger, updateSelectorManager);
        }

        #endregion

        #region Public Methods: Mutation

        public async Task<OperationResult> Create(FilterModel model)
            => await CreateManager.Create(model);

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
