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

        Task<string> GeneratePasswordResetToken(string email);

        Task ChangePasswordAsync(long userId, string oldPassword, string newPassword);

        Task AddToRolesAsync(long userId, IEnumerable<string> roleNames);

        Task RemoveFromRolesAsync(long userId, IEnumerable<string> roleNames);

        Task<TUserEntity> GetOneByEmail(string email);

        Task<string> GenerateEmailConfirmationToken(string email);

        Task<bool> ConfirmEmail(string email, string token);

        Task<TUserEntity> GetOneByLoginOrEmail(string loginOrEmail);
    }
}
