
using SavaDev.Base.User.Data.Entities;

namespace SavaDev.Base.Users.Security
{
    public interface ISecurityService<TKey, TUserEntity, TRoleEntity>
        where TUserEntity : BaseUser
        where TRoleEntity : BaseRole
    {
        string GetId();

        Task<bool> IsLocked(string userId);

        Task<bool> HasRequiredPermissions(string userId, IEnumerable<string> permissions);
    }
}
