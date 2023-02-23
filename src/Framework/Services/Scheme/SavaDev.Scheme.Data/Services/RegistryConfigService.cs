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
    public class RegistryConfigService : BaseEntityService<RegistryConfig, RegistryConfigModel>, IRegistryConfigService
    {
        #region Protected Properties: Managers

        protected CreateManager<RegistryConfig, RegistryConfigModel> CreateManager { get; }
        protected UpdateManager<long, RegistryConfig, RegistryConfigModel> UpdateManager { get; }
        protected RemoveManager<long, RegistryConfig> RemoveManager { get; }
        protected OneSelector<RegistryConfig> OneSelector { get; }
        protected AllSelector<long, RegistryConfig> AllSelector { get; } // TODO убрать бул

        #endregion

        #region Public Constructors

        public RegistryConfigService(ISchemeContext dbContext, IMapper mapper, ILogger<RegistryConfigService> logger)
            : base(dbContext, mapper, nameof(RegistryConfigService))
        {
            CreateManager = new CreateManager<RegistryConfig, RegistryConfigModel>(dbContext, mapper, logger);
            UpdateManager = new UpdateManager<long, RegistryConfig, RegistryConfigModel>(dbContext, mapper, logger);
            RemoveManager = new RemoveManager<long, RegistryConfig>(dbContext, mapper, logger);
            OneSelector = new OneSelector<RegistryConfig>(dbContext, mapper, logger);
            AllSelector = new AllSelector<long, RegistryConfig>(dbContext, mapper, logger);
        }
        #endregion

        #region Public Methods: Mutation

        public async Task<OperationResult> Create(RegistryConfigModel model)
            => await CreateManager.Create(model);

        public async Task<OperationResult> Update(long id, RegistryConfigModel model)
        {
            var result = await UpdateManager.Update(id, model);
            return result;
        }

        public async Task<OperationResult> Remove(long id)
            => await RemoveManager.Remove(id);

        #endregion

        public async Task<RegistryConfigModel> GetLast(Guid tableId)
        {
            var entity = await _dbContext.Set<RegistryConfig>().Where(m => m.TableId == tableId)
                .OrderByDescending(m => m.Id).FirstOrDefaultAsync();
            return _mapper.Map<RegistryConfigModel>(entity);
        }

        public async Task<RegistryConfigModel> GetOne(long id)
        {
            var model = await OneSelector.GetOne<RegistryConfigModel>(m => m.Id == id);
            return model;
        }

        public async Task<IEnumerable<RegistryConfigModel>> GetAll(Guid tableId)
        {
            var list = await _dbContext.Set<RegistryConfig>()
                .Where(m => m.TableId == tableId)
                .ProjectTo<RegistryConfigModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return list;
        }
    }
}
