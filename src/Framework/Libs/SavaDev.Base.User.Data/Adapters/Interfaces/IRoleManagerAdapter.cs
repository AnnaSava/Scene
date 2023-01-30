using SavaDev.Base.User.Data.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Adapters.Interfaces
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
