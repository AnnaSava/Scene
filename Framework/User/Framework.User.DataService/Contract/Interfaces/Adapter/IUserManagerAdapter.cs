using Framework.User.DataService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Interfaces
{
    public interface IUserManagerAdapter<TUserEntity> where TUserEntity : BaseUser
    {
        Task<TUserEntity> CreateAsync(TUserEntity user, string password);

        Task<TUserEntity> UpdateAsync(TUserEntity user);

        Task ChangePasswordAsync(long userId, string oldPassword, string newPassword);

        Task AddToRolesAsync(long userId, IEnumerable<string> roleNames);

        Task RemoveFromRolesAsync(long userId, IEnumerable<string> roleNames);

        Task<TUserEntity> GetOneByEmail(string email);
    }
}
