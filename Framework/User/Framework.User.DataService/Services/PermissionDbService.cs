using AutoMapper;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.DataService.Services;
using Framework.User.DataService.Contract.Interfaces.Context;
using Framework.User.DataService.Contract.Models;
using Framework.User.DataService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.User.DataService.Services.Filters;
using Framework.User.DataService.Contract.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Framework.User.DataService.Services
{
    public class PermissionDbService : AnyEntityService<Permission, PermissionModel>, IPermissionDbService
    {
        private readonly IPermissionContext _context;

        public PermissionDbService(IPermissionContext dbContext, IMapper mapper)
            : base(dbContext as IDbContext, mapper, nameof(PermissionDbService))
        {
            _context = dbContext;
        }

        public async Task<PermissionModel> GetOne(string name)
        {
            return await GetOne(m => m.Name == name.ToLower());
        }

        public async Task<PageListModel<PermissionModel>> GetAll(ListQueryModel<PermissionFilterModel> query)
        {
            // TODO Фильтрация по PermissionCultures

            return await _dbContext.GetAll<Permission, PermissionModel, PermissionFilterModel>(query, ApplyFilters, m => m.Name, _mapper);
        }

        public async Task<Dictionary<string, List<string>>> GetTree()
        {
            var treeDict = new Dictionary<string, List<string>>();

            var permissions = await _context.Permissions.ToListAsync();

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

            return await _context.Permissions.AnyAsync(m => m.Name == name.ToLower());
        }

        public async Task<IEnumerable<string>> FilterExisting(IEnumerable<string> names)
        {
            if (names == null || names.Count() == 0) return default;

            var normalizedNames = names.Select(m => m.ToLower());

            return await _context.Permissions.Where(m => normalizedNames.Contains(m.Name.ToLower()))
                .Select(m => m.Name)
                .ToListAsync();
        }

        protected void ApplyFilters(ref IQueryable<Permission> list, PermissionFilterModel filter)
        {
            list = list.ApplyFilters(filter);
        }
    }
}
