using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.User.Data.Adapters.Interfaces;
using SavaDev.Base.User.Data.Context;
using SavaDev.Base.User.Data.Entities;
using SavaDev.Base.User.Data.Manager;
using SavaDev.Base.User.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Services
{
    public class BaseAccountDbService<TKey, TEntity> where TEntity : BaseUser
    {
        protected readonly IDbContext _dbContext;
        private readonly ISignInManagerAdapter _signInManagerAdapter;
        protected readonly IMapper _mapper;
        private readonly UserManager<TEntity> _userManager;

        protected readonly AccountEntityManager<TKey, TEntity> entityManager;

        public BaseAccountDbService(
            IDbContext dbContext,
            UserManager<TEntity> userManager,
            ISignInManagerAdapter signInManagerAdapter,            
            IMapper mapper,
            ILogger logger)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManagerAdapter = signInManagerAdapter;
            _mapper = mapper;

            entityManager = new AccountEntityManager<TKey, TEntity>(dbContext, userManager, _mapper, logger);
        }

        public async Task<string> GeneratePasswordResetToken(string email)
            => await entityManager.GeneratePasswordResetToken(email);

        public async Task ResetPassword(string email, string token, string newPassword)
            => await entityManager.ResetPassword(email, token, newPassword);

        public async Task ChangePasswordAsync(TKey userId, string oldPassword, string newPassword)
            => await entityManager.ChangePasswordAsync(userId, oldPassword, newPassword);

        public async Task<IList<string>> GetRolesAsync(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<string> GenerateEmailConfirmationToken(string email) => await entityManager.GenerateEmailConfirmationToken(email);

        public async Task<bool> ConfirmEmail(string email, string token) => await entityManager.ConfirmEmail(email, token);

        public async Task<SignInResultModel<TUserOutModel>> SignIn<TUserOutModel>(LoginModel model)
            where TUserOutModel : BaseUserModel
        {

            // TODO
            //var userEntity = await entityManager.GetOneByLoginOrEmail(model.Identifier);

            //var result = await _signInManagerAdapter.SignIn(userEntity.Email, model.Password, model.RememberMe);

            var resultModel = new SignInResultModel<TUserOutModel>()
            {
               // Succeeded = result.Succeeded,
            };

            //if (result.Succeeded)
            //{
            //    var user = await entityManager.GetOneByLoginOrEmail(model.Identifier);
            //    resultModel.User = _mapper.Map<TUserOutModel>(user);
            //};
            return resultModel;
        }
    }
}
