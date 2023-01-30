using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Services;
using SavaDev.System.Data.Contract;
using SavaDev.System.Data.Contract.Models;
using SavaDev.System.Data.Entities;

namespace SavaDev.System.Data.Services
{
    public class PermissionDbService : BaseEntityService<Permission, PermissionModel>, IPermissionDbService
    {
        private readonly AnyEntityManager<Permission, PermissionModel> entityManager;

        public PermissionDbService(IDbContext dbContext, IMapper mapper, ILogger<PermissionDbService> logger)
            : base(dbContext, mapper, nameof(PermissionDbService))
        {
            entityManager = new AnyEntityManager<Permission, PermissionModel>(dbContext, mapper, logger);
        }

        public async Task<OperationResult<PermissionModel>> Create(PermissionModel model) => await entityManager.Create(model);

        public async Task<PermissionModel> GetOne(string name) => await entityManager.GetOne<PermissionModel>(m => m.Name == name.ToLower());
        
        //public async Task<PageListModel<PermissionModel>> GetAll(ListQueryModel<PermissionFilterModel> query)
        //{
        //    // TODO Фильтрация по PermissionCultures

        //    return await _dbContext.GetAll<Permission, PermissionModel, PermissionFilterModel>(query, ApplyFilters, m => m.Name, _mapper);
        //}

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

        public async Task<bool> CheckExists(string name)
        {
            if (name == null) throw new ArgumentNullException();

            return await entityManager.CheckExists(m => m.Name == name.ToLower());
        }

        public async Task<IEnumerable<string>> FilterExisting(IEnumerable<string> names)
        {
            if (names == null || names.Count() == 0) return default;

            var normalizedNames = names.Select(m => m.ToLower());

            return await _dbContext.Set<Permission>().Where(m => normalizedNames.Contains(m.Name.ToLower()))
                .Select(m => m.Name)
                .ToListAsync();
        }

        //protected void ApplyFilters(ref IQueryable<Permission> list, PermissionFilterModel filter)
        //{
        //    list = list.ApplyFilters(filter);
        //}
    }
}
