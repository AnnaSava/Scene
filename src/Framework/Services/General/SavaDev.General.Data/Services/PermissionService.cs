using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;
using SavaDev.General.Data.Contract;
using SavaDev.General.Data.Contract.Context;
using SavaDev.General.Data.Contract.Models;
using SavaDev.General.Data.Entities;

namespace SavaDev.General.Data.Services
{
    public class PermissionService : BaseEntityService<Permission, PermissionModel>, IPermissionService
    {
        #region Protected Properties: Managers

        protected CreateManager<Permission, PermissionModel> CreateManager { get; }
        protected OneSelector<Permission> OneSelector { get; }
        protected AllSelector<bool, Permission> AllSelector { get; } // TODO убрать бул

        #endregion

        #region Public Constructors

        public PermissionService(IGeneralContext dbContext, IMapper mapper, ILogger<PermissionService> logger)
            : base(dbContext, mapper, nameof(PermissionService))
        {
            CreateManager = new CreateManager<Permission, PermissionModel>(dbContext, mapper, logger);
            OneSelector = new OneSelector<Permission>(dbContext, mapper, logger);
            AllSelector = new AllSelector<bool, Permission>(dbContext, mapper, logger);
        }

        #endregion

        #region Public Methods: Mutation

        public async Task<OperationResult> Create(PermissionModel model) 
            => await CreateManager.Create(model);

        #endregion

        #region Public Methods: Query One

        public async Task<PermissionModel?> GetOne(string name) 
            => await OneSelector.GetOne<PermissionModel>(m => m.Name == name.ToLower());

        public async Task<bool> CheckExists(string name)
        {
            if (name == null) throw new ArgumentNullException();

            return await OneSelector.CheckExists(m => m.Name == name.ToLower());
        }

        #endregion

        #region Public Methods: Query All

        public async Task<RegistryPage<PermissionModel>> GetRegistryPage(RegistryQuery<PermissionFilterModel> query)
        {
            var page = await AllSelector.GetRegistryPage<PermissionFilterModel, PermissionModel>(query);
            return page;
        }

        public async Task<Dictionary<string, List<string>>> GetTree()
        {
            var treeDict = new Dictionary<string, List<string>>();

            var permissions = await _dbContext.Set<Permission>().ToListAsync();

            foreach(var permission in permissions)
            {
                if(permission.Name.Contains('.') && permission.Name.StartsWith('.') == false)
                {
                    var group = permission.Name.Substring(0, permission.Name.IndexOf('.'));

                    if(treeDict.ContainsKey(group))
                    {
                        treeDict[group].Add(permission.Name);
                    }
                    else
                    {
                        treeDict[group] = new List<string> { permission.Name };
                    }
                }
            }

            return treeDict;
        }

        public async Task<IEnumerable<string>> FilterExisting(IEnumerable<string> names)
        {
            if (names == null || names.Count() == 0) return default;

            var normalizedNames = names.Select(m => m.ToLower());

            return await _dbContext.Set<Permission>().Where(m => normalizedNames.Contains(m.Name.ToLower()))
                .Select(m => m.Name)
                .OrderBy(m => m)
                .ToListAsync();
        }

        #endregion
    }
}
