using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.User.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Manager
{
    public class RoleEntityManager<TKey, TEntity, TFormModel> 
        where TEntity : BaseRole
    {
        private readonly IDbContext _dbContext;
        private readonly RoleManager<TEntity> _roleManager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public RoleEntityManager(IDbContext dbContext, RoleManager<TEntity> roleManager, IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> CheckRoleNameExists<TRoleEntity>(string name)
            where TRoleEntity : BaseRole
        {
            if (string.IsNullOrEmpty(name)) return false;
            throw new NotImplementedException();
           // return await _dbContext.Set<TRoleEntity>().AnyAsync(m => m.NormalizedName == name.ToUpper());
        }
    }
}
