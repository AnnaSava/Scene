using Microsoft.AspNetCore.Identity;
using SavaDev.Base.User.Data.Adapters.Interfaces;
using SavaDev.Base.User.Data.Entities;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Adapters
{
    public class SignInManagerAdapter<TUserEntity> : ISignInManagerAdapter
        where TUserEntity : BaseUser
    {
        protected readonly UserManager<TUserEntity> _userManager;
        protected readonly SignInManager<TUserEntity> _signInManager;

        public SignInManagerAdapter(UserManager<TUserEntity> userManager,
            SignInManager<TUserEntity> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
