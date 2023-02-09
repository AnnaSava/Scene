
using SavaDev.Base.User.Data.Entities;

namespace SavaDev.Base.Users.Security
{
    public interface ISecurityService
    {
        string GetId();

        Task<bool> IsLocked(string userId);

        Task<bool> HasRequiredPermissions(string userId, IEnumerable<string> permissions);
    }
}
