using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Models;
using SavaDev.Base.Data.Services;
using SavaDev.Scheme.Contract.Models;
using SavaDev.Scheme.Data.Contract;
using SavaDev.Scheme.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Data.Services
{
    public class ColumnService : BaseEntityService<Column, ColumnModel>, IColumnService
    {
        private readonly CreateManager<Column, ColumnModel> creatorManager;
        private readonly UpdateManager<Guid, Column, ColumnModel> updaterManager;
        private readonly UpdateSelector<Guid, Column> updateSelectorManager;

        #region Constructors

        public ColumnService(SchemeContext dbContext, IMapper mapper, ILogger<ColumnService> logger)
            : base(dbContext, mapper, nameof(ColumnService))
        {
            creatorManager = new CreateManager<Column, ColumnModel>(dbContext, mapper, logger);
            updateSelectorManager = new UpdateSelector<Guid, Column>(dbContext, mapper, logger);
            updaterManager = new UpdateManager<Guid, Column, ColumnModel>(dbContext, mapper, logger, updateSelectorManager);
        }

        #endregion

        public async Task<OperationResult> Create(ColumnModel model)
            => await creatorManager.Create(model);

        public async Task<IEnumerable<ColumnModel>> GetAll(ModelPlacement modelPlacement)
        {
            var list = await _dbContext.Set<Column>().Include(m => m.Table).Include(m => m.Properties)
                .Where(m => m.Table.Entity == modelPlacement.Entity && m.Table.Module == modelPlacement.Module && m.IsDisplayed)
                .ProjectTo<ColumnModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return list;
        }

        public async Task<IEnumerable<ColumnModel>> GetAll(Guid tableId)
        {            
            var list = await _dbContext.Set<Column>()
                .Where(m => m.TableId == tableId)// && m.IsDisplayed)
                .ProjectTo<ColumnModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return list;
        }
    }
}
