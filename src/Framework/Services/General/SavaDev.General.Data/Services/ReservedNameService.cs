using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Models.Interfaces;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;
using SavaDev.General.Data.Contract;
using SavaDev.General.Data.Contract.Context;
using SavaDev.General.Data.Contract.Models;
using SavaDev.General.Data.Entities;

namespace SavaDev.General.Data.Services
{
    public class ReservedNameService : BaseEntityService<ReservedName, ReservedNameModel>, IReservedNameService
    {
        #region Protected Properties: Managers

        protected CreateManager<ReservedName, ReservedNameModel> CreateManager { get; }
        protected UpdateAnyManager<ReservedName, ReservedNameModel> UpdateManager { get; }
        protected RemoveManager<ReservedName> RemoveManager { get; }
        protected OneSelector<ReservedName> OneSelector { get; }
        protected AllSelector<bool, ReservedName> AllSelector { get; } // TODO убрать бул

        #endregion

        #region Public Constructors

        public ReservedNameService(IGeneralContext dbContext, IMapper mapper, ILogger<ReservedNameService> logger)
            : base(dbContext, mapper, nameof(ReservedNameService))
        {
            CreateManager = new CreateManager<ReservedName, ReservedNameModel>(dbContext, mapper, logger);
            UpdateManager = new UpdateAnyManager<ReservedName, ReservedNameModel>(dbContext, mapper, logger);
            RemoveManager = new RemoveManager<ReservedName>(dbContext, mapper, logger);
            OneSelector = new OneSelector<ReservedName>(dbContext, mapper, logger);
            AllSelector = new AllSelector<bool, ReservedName>(dbContext, mapper, logger);
        }

        #endregion

        #region Public Methods: Mutation

        public async Task<OperationResult> Create(ReservedNameModel model) 
            => await CreateManager.Create(model, validate: Validate);
        public async Task<OperationResult> Update(ReservedNameModel model) 
            => await UpdateManager.Update(m => m.Text == model.Text, model);
        public async Task<OperationResult> Remove(string text) 
            => await RemoveManager.Remove(m => m.Text == text.ToLower());

        #endregion

        #region Public Methods: Query One

        public async Task<ReservedNameModel> GetOne(string text) 
            => await OneSelector.GetOne<ReservedNameModel>(m => m.Text == text.ToLower());

        public async Task<bool> CheckIsReserved(string text)
        {
            if (text == null) throw new ArgumentNullException();

            var normalizedText = text.ToLower();
            return await _dbContext.Set<ReservedName>()
                  .Select(m => m.Text)
                  .Union(_dbContext.Set<ReservedName>()
                  .Where(m => m.IncludePlural)
                  .Select(m => m.Text + "s"))
                  .AnyAsync(m => m == normalizedText);
        }

        public async Task<bool> CheckExists(string text)
        {
            if (text == null) throw new ArgumentNullException();

            return await OneSelector.CheckExists(m => m.Text == text.ToLower());
        }

        #endregion

        #region Public Methods: Query All

        public async Task<RegistryPage<ReservedNameModel>> GetRegistryPage(RegistryQuery<ReservedNameFilterModel> query)
        {
            var page = await AllSelector.GetRegistryPage<ReservedNameFilterModel, ReservedNameModel>(query);
            return page;
        }

        #endregion

        #region Private Methods

        private async Task Validate(IFormModel model)
        {
            var docModel = model as ReservedNameModel;
            if (docModel?.Text == null)
            {
                throw new ArgumentNullException("Text is null.");
            }
            var exists = await CheckExists(docModel.Text);
            if (exists)
            {
                throw new InvalidOperationException($"Reserved name {docModel.Text} already exists.");
            }
        }

        #endregion
    }
}
