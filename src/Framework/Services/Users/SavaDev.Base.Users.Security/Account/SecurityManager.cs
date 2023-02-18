using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.User.Data.Entities;
using SavaDev.Base.User.Data.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Manager
{
    public class SecurityManager<TKey, TEntity>
        where TEntity : BaseUser
    {
        private readonly IDbContext _dbContext;
        private readonly UserManager<TEntity> _userManager;
        private readonly SignInManager<TEntity> _signInManager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public SecurityManager(IDbContext dbContext, UserManager<TEntity> userManager, SignInManager<TEntity> signInManager, IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _logger = logger;
        }

        #region Sign In

        public async Task<SignInResult> SignIn(string identifier, string password, bool rememberMe)
        {
            if (identifier.Contains("@"))
            {
                var user = await _userManager.FindByEmailAsync(identifier);
                return await _signInManager.PasswordSignInAsync(user, password, rememberMe, false);
            }
            return await _signInManager.PasswordSignInAsync(identifier, password, rememberMe, false);
        }

        public async Task<SignInResult> SignIn(TEntity user, string password, bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(user, password, rememberMe, false);
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<SignInResult> CheckPasswordSignIn(string email, string password)
        {
            var entity = await _userManager.FindByEmailAsync(email);
            // TODO проверка на нулл?
            return await _signInManager.CheckPasswordSignInAsync(entity, password, false);
        }

        #endregion

        public async Task<string> GeneratePasswordResetToken(string email)
        {
            var user = await GetOneByEmail(email);
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task ResetPassword(string email, string token, string newPassword)
        {
            var user = await GetOneByEmail(email);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task ChangePasswordAsync(TKey userId, string oldPassword, string newPassword)
        {
            var user = await FindForUpdate(userId);
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<IList<string>> GetRolesAsync(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<string> GenerateEmailConfirmationToken(string email)
        {
            var user = await GetOneByEmail(email);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
            var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
            return codeEncoded;
        }

        public async Task<bool> ConfirmEmail(string email, string token)
        {
            var codeDecodedBytes = WebEncoders.Base64UrlDecode(token);
            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);

            var user = await GetOneByEmail(email);
            var result = await _userManager.ConfirmEmailAsync(user, codeDecoded);

            return user.EmailConfirmed;
        }

        private async Task<TEntity> FindForUpdate(TKey userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null || user.IsDeleted) throw new UserNotFoundException();

            return user;
        }

        public async Task<TEntity> GetOneByEmail(string email)
        {
            var entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(m => m.Email == email && m.IsDeleted == false);
            return entity;
        }
    }
}
