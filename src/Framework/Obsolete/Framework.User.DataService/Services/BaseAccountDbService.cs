using AutoMapper;
using Framework.Base.Exceptions;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Models;
using Framework.User.DataService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Services
{
    public class BaseAccountDbService<TUserEntity> where TUserEntity : BaseUser
    {
        protected readonly IUserContext<TUserEntity> _dbContext;
        protected IUserManagerAdapter<TUserEntity> _userManagerAdapter;
        private readonly ISignInManagerAdapter _signInManagerAdapter;
        protected readonly IMapper _mapper;

        public BaseAccountDbService(
            IUserContext<TUserEntity> dbContext,
            IUserManagerAdapter<TUserEntity> userManagerAdapter,
            ISignInManagerAdapter signInManagerAdapter,
            IMapper mapper)
        {
            _dbContext = dbContext;

            _userManagerAdapter = userManagerAdapter;

            _signInManagerAdapter = signInManagerAdapter;

            _mapper = mapper ?? throw new ProjectArgumentException(
                GetType(),
                nameof(BaseUserDbService<TUserEntity>),
                nameof(mapper),
                null);
        }

        public async Task<string> GenerateEmailConfirmationToken(string email)
        {
            return await _userManagerAdapter.GenerateEmailConfirmationToken(email);
        }

        public async Task<bool> ConfirmEmail(string email, string token)
        {
            return await _userManagerAdapter.ConfirmEmail(email, token);
        }

        public async Task<string> GeneratePasswordResetToken(string email)
        {
            return await _userManagerAdapter.GeneratePasswordResetToken(email);
        }

        public async Task ResetPassword(string email, string token, string newPassword)
        {
            await _userManagerAdapter.ResetPassword(email, token, newPassword);
        }

        public async Task<SignInResultModel<TUserOutModel>> SignIn<TUserOutModel>(LoginModel model)
            where TUserOutModel : BaseUserModel
        {
            var userEntity = await _userManagerAdapter.GetOneByLoginOrEmail(model.Identifier);

            var result = await _signInManagerAdapter.SignIn(userEntity.Email, model.Password, model.RememberMe);

            var resultModel = new SignInResultModel<TUserOutModel>()
            {
                Succeeded = result.Succeeded,
            };

            if (result.Succeeded)
            {
                var user = await _userManagerAdapter.GetOneByLoginOrEmail(model.Identifier);
                resultModel.User = _mapper.Map<TUserOutModel>(user);
            };
            return resultModel;
        }
    }
}
