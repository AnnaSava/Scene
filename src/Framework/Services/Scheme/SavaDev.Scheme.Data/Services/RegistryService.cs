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
    public class RegistryService : BaseEntityService<Registry, RegistryModel>, IRegistryService
    {
        private readonly CreateManager<Registry, RegistryModel> creatorManager;

        #region Constructors

        public RegistryService(SchemeContext dbContext, IMapper mapper, ILogger<RegistryService> logger) 
            : base(dbContext, mapper, nameof(RegistryService))
        {
            creatorManager = new CreateManager<Registry, RegistryModel>(dbContext, mapper, logger);
        }

        #endregion

        public async Task<OperationResult> Create(RegistryModel model)
            => await creatorManager.Create(model);

        public async Task<RegistryModel> GetOneByPlacement(ModelPlacement placement)
        {
            var entity = await _dbContext.Set<Registry>().FirstOrDefaultAsync(m => m.Entity == placement.Entity && m.Module == placement.Module);
            return _mapper.Map<RegistryModel>(entity);
        }
    }
}
