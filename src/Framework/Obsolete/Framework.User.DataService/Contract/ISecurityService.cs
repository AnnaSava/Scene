using Framework.User.DataService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract
{
    public interface ISecurityService<TUserEntity, TRoleEntity>
        where TUserEntity : BaseUser
        where TRoleEntity : BaseRole
    {
        Task<bool> IsLocked(string userId);

        Task<bool> HasRequiredPermissions(string userId, IEnumerable<string> permissions);
    }
}
