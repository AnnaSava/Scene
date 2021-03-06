using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Entities;
using Framework.User.DataService.Exceptions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Services
{
    public class UserManagerAdapter<TUserEntity> : IUserManagerAdapter<TUserEntity> 
        where TUserEntity : BaseUser
    {
        private readonly UserManager<TUserEntity> _userManager;

        public UserManagerAdapter(UserManager<TUserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<TUserEntity> CreateAsync(TUserEntity user, string password)
        {
            user.RegDate = user.LastUpdated = DateTime.Now;
            var result = await _userManager.CreateAsync(user, password);
            HandleResult(result);
            return user;
        }

        public async Task<TUserEntity> UpdateAsync(TUserEntity user)
        {
            user.LastUpdated = DateTime.Now;
            var result = await _userManager.UpdateAsync(user);
            HandleResult(result);
            return user;
        }

        public async Task ChangePasswordAsync(long userId, string oldPassword, string newPassword)
        {
            var user = await FindForUpdate(userId);
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            HandleResult(result);
        }

        public async Task AddToRolesAsync(long userId, IEnumerable<string> roleNames)
        {
            var user = await FindForUpdate(userId);
            var result = await _userManager.AddToRolesAsync(user, roleNames);
            HandleResult(result);
        }

        public async Task RemoveFromRolesAsync(long userId, IEnumerable<string> roleNames)
        {
            var user = await FindForUpdate(userId);
            var result = await _userManager.RemoveFromRolesAsync(user, roleNames);
            HandleResult(result);
        }

        public async Task<TUserEntity> GetOneByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        private async Task<TUserEntity> FindForUpdate(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null || user.IsDeleted) throw new UserNotFoundException();

            return user;
        }

        private void HandleResult(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.First().Description);
            }
        }
    }
}
