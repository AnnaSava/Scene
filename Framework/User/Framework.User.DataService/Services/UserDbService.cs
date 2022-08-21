using AutoMapper;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.Exceptions;
using Framework.Base.Types.ModelTypes;
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
    // TODO Подумать о вынесении в отдельную сборку
    public class UserDbService<TUserEntity>
        where TUserEntity : BaseUser
    {
        protected readonly IUserContext<TUserEntity> _dbContext;
        protected IUserManagerAdapter<TUserEntity> _userManagerAdapter;
        private readonly ISignInManagerAdapter _signInManagerAdapter;
        protected readonly IMapper _mapper;

        public UserDbService(
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
                nameof(UserDbService<TUserEntity>),
                nameof(mapper),
                null);
        }

        public async Task<TUserModel> Create<TUserModel>(TUserModel model, string password)
        {
            return await _userManagerAdapter.CreateUser<TUserEntity, TUserModel, TUserModel>(model, password, _mapper);
        }

        public async Task<TUserModelOut> Create<TUserModelIn, TUserModelOut>(TUserModelIn model, string password)
        {
            return await _userManagerAdapter.CreateUser<TUserEntity, TUserModelIn, TUserModelOut>(model, password, _mapper);
        }

        public async Task<TUserModelOut> Update<TUserModelIn, TUserModelOut>(TUserModelIn model)
            where TUserModelIn : IModel<long>
        {
            return await _userManagerAdapter.UpdateUser<TUserEntity, TUserModelIn, TUserModelOut>(model, _dbContext, _mapper);
        }

        public async Task<TUserModelOut> Delete<TUserModelOut>(long id)
        {
            return await _userManagerAdapter.Delete<TUserEntity, TUserModelOut>(id, _dbContext, _mapper);
        }

        public async Task<TUserModelOut> Restore<TUserModelOut>(long id)
        {
            return await _userManagerAdapter.Restore<TUserEntity, TUserModelOut>(id, _dbContext, _mapper);
        }

        public async Task<string> GenerateEmailConfirmationToken(string email)
        {
            return await _userManagerAdapter.GenerateEmailConfirmationToken(email);
        }

        public async Task<bool> ConfirmEmail(string email, string token)
        {
            return await _userManagerAdapter.ConfirmEmail(email, token);
        }

        public async Task<TUserModelOut> GetOneByLoginOrEmail<TUserModelOut>(string loginOrEmail)
        {
            var user = await _userManagerAdapter.GetOneByLoginOrEmail(loginOrEmail);
            return _mapper.Map<TUserModelOut>(user);
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
            var userEntity = await _userManagerAdapter.GetOneByLoginOrEmail(model.LoginOrEmail);

            var result = await _signInManagerAdapter.CheckPasswordSignIn(userEntity.Email, model.Password);

            var resultModel = new SignInResultModel<TUserOutModel>()
            {
                Succeeded = result.Succeeded,
            };

            if(result.Succeeded)
            {
                var user = await _userManagerAdapter.GetOneByLoginOrEmail(model.LoginOrEmail);
                resultModel.User = _mapper.Map<TUserOutModel>(user);
            };
            return resultModel;
        }

        // TODO подумать, куда перенести, т.к. в теории может пригодиться не только для пользователей
        protected IEnumerable<ListSortModel> GetChangedSortFields(IEnumerable<ListSortModel> sortList, Dictionary<string, string> diff)
        {
            if (sortList == null) return null;

            var newSortList = new List<ListSortModel>();

            foreach (var sort in sortList)
            {
                var newSort = sort;
                var fieldName = sort.FieldName.ToLower();
                if (diff.ContainsKey(fieldName))
                {
                    newSort.FieldName = diff[fieldName];
                }

                newSortList.Add(newSort);
            }
            return newSortList;
        }
    }
}
