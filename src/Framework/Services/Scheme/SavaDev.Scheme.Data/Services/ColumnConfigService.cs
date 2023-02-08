using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Services;
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
    public class ColumnConfigService : BaseEntityService<ColumnConfig, ColumnConfigModel>, IColumnConfigService
    {
        #region Protected Properties: Managers

        protected CreateManager<ColumnConfig, ColumnConfigModel> CreateManager { get; }
        protected UpdateSelector<long, ColumnConfig> UpdateSelector { get; }
        protected RemoveManager<long, ColumnConfig> RemoveManager { get; }
        protected OneSelector<ColumnConfig> OneSelector { get; }
        protected AllSelector<long, ColumnConfig> AllSelector { get; } // TODO убрать бул

        #endregion

        #region Public Constructors

        public ColumnConfigService(ISchemeContext dbContext, IMapper mapper, ILogger<ColumnConfigService> logger)
            : base(dbContext, mapper, nameof(ColumnConfigService))
        {
            CreateManager = new CreateManager<ColumnConfig, ColumnConfigModel>(dbContext, mapper, logger);
            UpdateSelector = new UpdateSelector<long, ColumnConfig>(dbContext, mapper, logger);
            RemoveManager = new RemoveManager<long, ColumnConfig>(dbContext, mapper, logger, UpdateSelector);
            OneSelector = new OneSelector<ColumnConfig>(dbContext, mapper, logger);
            AllSelector = new AllSelector<long, ColumnConfig>(dbContext, mapper, logger);
        }
        #endregion

        #region Public Methods: Mutation

        public async Task<OperationResult> Create(ColumnConfigModel model)
            => await CreateManager.Create(model);

        public async Task<OperationResult> Remove(long id)
            => await RemoveManager.Remove(id);

        #endregion

        public async Task<ColumnConfigModel> GetLast(Guid tableId)
        {
            var entity = await _dbContext.Set<ColumnConfig>().Where(m => m.TableId == tableId)
                .OrderByDescending(m => m.Id).FirstOrDefaultAsync();
            return _mapper.Map<ColumnConfigModel>(entity);
        }

        public async Task<IEnumerable<ColumnConfigModel>> GetAll(Guid tableId)
        {
            var list = await _dbContext.Set<ColumnConfig>()
                .Where(m => m.TableId == tableId)
                .ProjectTo<ColumnConfigModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return list;
        }
    }
}
