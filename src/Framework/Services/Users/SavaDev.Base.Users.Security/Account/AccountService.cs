using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.User.Data.Entities;
using SavaDev.Base.User.Data.Manager;
using SavaDev.Base.User.Data.Models;
using SavaDev.Base.Users.Data.Manager;
using SavaDev.Base.Users.Security.Contract;

namespace SavaDev.Base.Users.Security.Account
{
    public class AccountService<TKey, TEntity> : IAccountService
        where TEntity : BaseUser
    {
        protected readonly IDbContext _dbContext;     
        private readonly UserManager<TEntity> _userManager;
        protected readonly IMapper _mapper;

        protected readonly SecurityManager<TKey, TEntity> securityManager;

        public AccountService(
            IDbContext dbContext,
            UserManager<TEntity> userManager,
            SignInManager<TEntity> signInManager,            
            IMapper mapper,
            ILogger<AccountService<TKey, TEntity>> logger)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;

            securityManager = new SecurityManager<TKey, TEntity>(dbContext, userManager, signInManager, _mapper, logger);
        }

        public async Task<string> GeneratePasswordResetToken(string email)
            => await securityManager.GeneratePasswordResetToken(email);

        public async Task ResetPassword(string email, string token, string newPassword)
            => await securityManager.ResetPassword(email, token, newPassword);

        public async Task ChangePasswordAsync(TKey userId, string oldPassword, string newPassword)
            => await securityManager.ChangePasswordAsync(userId, oldPassword, newPassword);

        public async Task<IList<string>> GetRolesAsync(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<string> GenerateEmailConfirmationToken(string email) => await securityManager.GenerateEmailConfirmationToken(email);

        public async Task<bool> ConfirmEmail(string email, string token) => await securityManager.ConfirmEmail(email, token);

        public async Task<SignInResultModel<TUserOutModel>> SignIn<TUserOutModel>(LoginModel model)
            where TUserOutModel : BaseUserModel
        {
            var userEntity = await _userManager.GetOneByLoginOrEmail(model.Identifier);
            var result = await securityManager.SignIn(userEntity, model.Password, model.RememberMe);

            var resultModel = new SignInResultModel<TUserOutModel>()
            {
                Succeeded = result.Succeeded,
                User = result.Succeeded ? _mapper.Map<TUserOutModel>(userEntity) : null
            };

            return resultModel;
        }

        public async Task SignOut() => await securityManager.SignOut();

        public async Task<SignInResult> CheckPasswordSignIn(string email, string password) 
            => await securityManager.CheckPasswordSignIn(email, password);
    }
}
