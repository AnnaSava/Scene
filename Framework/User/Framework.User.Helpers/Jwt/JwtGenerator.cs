using Framework.Base.Exceptions;
using Framework.User.Helpers.Jwt.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Framework.User.Helpers.Jwt
{
    public class JwtGenerator
    {
        private const string AuthHeaderStartText = "Bearer ";
        private const string TokenPartsSeparator = ".";
        private const int TokenPartIndex = 1;
        private const char TokenEndFiller = '=';
        private const int TokenLengthMultiplicity = 4;

        private readonly DateTime now = DateTime.Now;
        private readonly long unixTime;
        private readonly JwtAppOptions _options;

        public JwtGenerator(JwtAppOptions options)
        {
            _options = options ?? throw new ProjectArgumentException(
                GetType(),
                nameof(JwtGenerator),
                nameof(options),
                null);

            unixTime = ((DateTimeOffset)now).ToUnixTimeSeconds();
        }

        public TokenPair CreateTokens(string userName)
        {
            var authExpiresRefreshStarts = now.Add(_options.GetLifeTime());
            var authToken = CreateToken(userName, now, authExpiresRefreshStarts);
            var refreshExpires = now.Add(_options.GetRefreshLifeTime());
            var refreshToken = CreateToken(userName, authExpiresRefreshStarts, refreshExpires);
            return new TokenPair(authToken, refreshToken, now);
        }

        public CredentialsModel GetCredentialsFromAuthHeader(string authHeader)
        {
            var token = authHeader.Substring(AuthHeaderStartText.Length).Trim();
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Некорректный формат токена");
            }

            var tokenParts = token.Split(TokenPartsSeparator);
            if (tokenParts.Length == 0)
            {
                throw new Exception("Некорректный формат токена");
            }

            var formattedToken = FormatTokenString(tokenParts[TokenPartIndex]);
            var credentialString = Encoding.UTF8.GetString(Convert.FromBase64String(formattedToken));
            var model = JsonSerializer.Deserialize<CredentialsModel>(credentialString);
            return model;
        }

        private TokenData CreateToken(string userName, DateTime notBefore, DateTime expires)
        {
            var token = new TokenData();

            var claims = new List<Claim>() {
                    new Claim(JwtRegisteredClaimNames.NameId, userName),
                    new Claim(JwtRegisteredClaimNames.Jti, token.Jti),
                    new Claim(JwtRegisteredClaimNames.Iat, unixTime.ToString(), ClaimValueTypes.Integer64)};

            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: _options.Issuer,
                    audience: _options.Audience,
                    claims: claims,
                    notBefore: notBefore,
                    expires: expires,
                    signingCredentials: new SigningCredentials(_options.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            token.Token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token;
        }

        /// <summary>
        /// Форматирование строки токена: если длина строки не является кратной 4, дополняем ее символами равенства
        /// </summary>
        /// <param name="tokenString">Строка с токеном</param>
        /// <returns>Отформатированная строка с токеном</returns>
        private string FormatTokenString(string tokenString)
        {
            var mod = tokenString.Length % TokenLengthMultiplicity;

            if (mod != 0)
            {
                tokenString = tokenString.PadRight((tokenString.Length + (TokenLengthMultiplicity - mod)), TokenEndFiller);
            }

            return tokenString;
        }
    }
}
