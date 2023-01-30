using SavaDev.Base.User.Data.Models;
using System;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Services.Interfaces
{
    public interface IAuthDbService
    {
        Task<AuthTokenModel> CreateToken(AuthTokenModel model);

        Task<AuthTokenModel> GetTokenByRefreshJti(string refreshJti);

        Task<bool> CheckRefreshTokenExists(string refreshJti);

        Task UpdateToken(string refreshJti, string newAuthJti, string newRefreshJti, DateTime updated);
    }
}
