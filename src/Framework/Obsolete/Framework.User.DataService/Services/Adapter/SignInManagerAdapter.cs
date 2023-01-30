using Framework.Base.Exceptions;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Services
{
    public class SignInManagerAdapter<TUserEntity> : ISignInManagerAdapter
        where TUserEntity : BaseUser
    {
        protected readonly UserManager<TUserEntity> _userManager;
        protected readonly SignInManager<TUserEntity> _signInManager;

        public SignInManagerAdapter(UserManager<TUserEntity> userManager,
            SignInManager<TUserEntity> signInManager)
        {
            _userManager = userManager ?? throw new ProjectArgumentException(
                GetType(),
                nameof(SignInManagerAdapter<TUserEntity>),
                nameof(userManager),
                null);

            _signInManager = signInManager ?? throw new ProjectArgumentException(
               GetType(),
               nameof(SignInManagerAdapter<TUserEntity>),
               nameof(signInManager),
               null);
        }

        public async Task<SignInResult> SignIn(string identifier, string password, bool rememberMe)
        {
            if (identifier.Contains("@"))
            {
                var user = await _userManager.FindByEmailAsync(identifier);
                return await _signInManager.PasswordSignInAsync(user, password, rememberMe, false);
            }
            return await _signInManager.PasswordSignInAsync(identifier, password, rememberMe, false);
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
    }
}
