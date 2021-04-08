using AutoMapper;
using Framework.Base.Exceptions;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Models;
using Framework.User.Helpers.Jwt;
using Framework.User.Service.Contract.Interfaces;
using Framework.User.Service.Contract.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Threading.Tasks;

namespace Framework.User.Service.Services
{
    public class AuthService<TUserModel> : IAuthService
        where TUserModel : BaseUserModel
    {
        private readonly IAuthDbService _authDbService;
        private readonly IUserDbService<TUserModel> _userDbService;
        private readonly JwtGenerator _jwtGenerator;
        private readonly IMapper _mapper;

        public AuthService(IAuthDbService authDbService, IUserDbService<TUserModel> userService, JwtGenerator jwtGenerator, IMapper mapper)
        {
            _authDbService = authDbService ?? throw new ProjectArgumentException(
                GetType(),
                nameof(AuthService<TUserModel>),
                nameof(authDbService),
                null);

            _userDbService = userService ?? throw new ProjectArgumentException(
                GetType(),
                nameof(AuthService<TUserModel>),
                nameof(userService),
                null);

            _jwtGenerator = jwtGenerator ?? throw new ProjectArgumentException(
                GetType(),
                nameof(AuthService<TUserModel>),
                nameof(jwtGenerator),
                null);

            _mapper = mapper ?? throw new ProjectArgumentException(
                GetType(),
                nameof(AuthService<TUserModel>),
                nameof(mapper),
                null);
        }

        public async Task<AuthorizedViewModel> Authorize(AuthorizingViewModel model)
        {
            var user = await _userDbService.GetOneByEmail(model.Email);
            var result = await _userDbService.CheckPasswordSignIn(user, model.Password);

            if (result.Succeeded)
            {
                var tokens = _jwtGenerator.CreateTokens(user.UserName);

                await _authDbService.CreateToken(new AuthTokenModel()
                {
                    UserId = user.Id,
                    AuthJti = tokens.AuthToken.Jti,
                    RefreshJti = tokens.RefreshToken.Jti,
                    DateCreated = tokens.DateCreated
                });

                return new AuthorizedViewModel
                {
                    Name = user.UserName,
                    Token = tokens.AuthToken.Token,
                    RefreshToken = tokens.RefreshToken.Token
                };
            }

            throw new Exception($"Пользователь {model.Email} не авторизован");
        }

        public async Task<AuthorizedViewModel> RefreshToken(string authHeader)
        {
            if (authHeader.StartsWith(JwtBearerDefaults.AuthenticationScheme) == false)
                throw new Exception("Некорректный формат токена"); // TODO: типизированную ошибку

            var credentials = _jwtGenerator.GetCredentialsFromAuthHeader(authHeader);

            if (await _authDbService.CheckRefreshTokenExists(credentials.jti) == false)
                throw new Exception("Токен не найден"); // TODO: типизированную ошибку

            var tokens = _jwtGenerator.CreateTokens(credentials.nameid);

            await _authDbService.UpdateToken(credentials.jti, tokens.AuthToken.Jti, tokens.RefreshToken.Jti, tokens.DateCreated);

            return new AuthorizedViewModel
            {
                Name = credentials.nameid,
                Token = tokens.AuthToken.Token,
                RefreshToken = tokens.RefreshToken.Token
            };

        }
    }
}
