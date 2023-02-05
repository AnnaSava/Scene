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
        private readonly EditableEntityManager<Guid, Column, ColumnModel> entityManager;

        #region Constructors

        public ColumnService(SchemeContext dbContext, IMapper mapper, ILogger<ColumnService> logger)
            : base(dbContext, mapper, nameof(ColumnService))
        {
            entityManager = new EditableEntityManager<Guid, Column, ColumnModel>(dbContext, mapper, logger);
        }

        #endregion

        public async Task<OperationResult> Create(ColumnModel model)
            => await entityManager.Create(model);

        public async Task<IEnumerable<ColumnModel>> GetAll(ModelPlacement modelPlacement)
        {
            var list = await _dbContext.Set<Column>().Include(m => m.Table).Include(m => m.Properties)
                .Where(m => m.Table.Entity == modelPlacement.Entity && m.Table.Module == modelPlacement.Module)
                .ProjectTo<ColumnModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return list;
        }
    }
}
