using AutoMapper;
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
    public class TableService : BaseEntityService<Table, TableModel>, ITableService
    {
        private readonly CreateManager<Table, TableModel> creatorManager;

        #region Constructors

        public TableService(SchemeContext dbContext, IMapper mapper, ILogger<TableService> logger) 
            : base(dbContext, mapper, nameof(TableService))
        {
            creatorManager = new CreateManager<Table, TableModel>(dbContext, mapper, logger);
        }

        #endregion

        public async Task<OperationResult> Create(TableModel model)
            => await creatorManager.Create(model);

        public async Task<TableModel> GetOneByPlacement(ModelPlacement placement)
        {
            var entity = await _dbContext.Set<Table>().FirstOrDefaultAsync(m => m.Entity == placement.Entity && m.Module == placement.Module);
            return _mapper.Map<TableModel>(entity);
        }
    }
}
