using Framework.User.DataService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Interfaces
{
    public interface IRoleManagerAdapter<TRoleEntity> 
        where TRoleEntity : BaseRole
    {
        Task<TRoleEntity> CreateAsync(TRoleEntity role);

        Task<TRoleEntity> UpdateAsync(TRoleEntity role);

        Task<TRoleEntity> GetRoleByNameAsync(string name);

        Task CreatePermissions(TRoleEntity role, IEnumerable<string> permissions);

        Task UpdatePermissions(TRoleEntity role, IEnumerable<string> permissions);

        Task<IEnumerable<Claim>> GetClaims(TRoleEntity role);
    }
}
