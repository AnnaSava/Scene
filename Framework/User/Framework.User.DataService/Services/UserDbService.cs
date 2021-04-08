using AutoMapper;
using Framework.Base.Exceptions;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Exceptions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Framework.User.DataService.Services
{
    public class UserDbService<TUserEntity, TUserModel> : IUserDbService<TUserModel>
        where TUserEntity : IdentityUser<long>
    {
        protected readonly UserManager<TUserEntity> _userManager;
        protected readonly SignInManager<TUserEntity> _signInManager;
        protected readonly IMapper _mapper;

        public UserDbService(
            UserManager<TUserEntity> userManager,
            SignInManager<TUserEntity> signInManager,
            IMapper mapper,
            string serviceName
            )
        {
            _userManager = userManager ?? throw new ProjectArgumentException(
                GetType(),
                serviceName,
                nameof(userManager),
                null);

            _signInManager = signInManager ?? throw new ProjectArgumentException(
               GetType(),
               serviceName,
               nameof(signInManager),
               null);

            _mapper = mapper ?? throw new ProjectArgumentException(
                GetType(),
                serviceName,
                nameof(mapper),
                null);
        }

        public async Task<TUserModel> CreateAsync(TUserModel model, string password)
        {
            var entity = _mapper.Map<TUserEntity>(model);
            IdentityResult identityResult = await _userManager.CreateAsync(entity, password);

            if (!identityResult.Succeeded)
            {
                throw new Exception(identityResult.Errors.First().Description);
            }

            return _mapper.Map<TUserModel>(entity);
        }

        public async Task<SignInResult> SignIn(string email, string password, bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(email, password, rememberMe, false);
        }

        public async Task SignIn(TUserModel user, bool rememberMe)
        {
            await _signInManager.SignInAsync(_mapper.Map<TUserEntity>(user), rememberMe);
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<TUserModel> GetOneByEmail(string email)
        {
            var entity = await _userManager.FindByEmailAsync(email);

            if (entity == null)
                throw new UserNotFoundException(
                          GetType(),
                          nameof(GetOneByEmail),
                          email);

            return _mapper.Map<TUserModel>(entity);
        }

        public async Task<SignInResult> CheckPasswordSignIn(TUserModel model, string password)
        {
            var entity = _mapper.Map<TUserEntity>(model);
            return await _signInManager.CheckPasswordSignInAsync(entity, password, false);
        }
    }
}
