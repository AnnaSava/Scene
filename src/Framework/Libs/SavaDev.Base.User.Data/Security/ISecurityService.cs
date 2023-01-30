using SavaDev.Base.User.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Security
{
    public interface ISecurityService<TUserEntity, TRoleEntity>
        where TUserEntity : BaseUser
        where TRoleEntity : BaseRole
    {
        Task<bool> IsLocked(string userId);

        Task<bool> HasRequiredPermissions(string userId, IEnumerable<string> permissions);
    }
}
