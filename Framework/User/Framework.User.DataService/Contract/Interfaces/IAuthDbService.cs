using Framework.User.DataService.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Interfaces
{
    public interface IAuthDbService
    {
        Task<AuthTokenModel> CreateToken(AuthTokenModel model);

        Task<AuthTokenModel> GetTokenByRefreshJti(string refreshJti);

        Task<bool> CheckRefreshTokenExists(string refreshJti);

        Task UpdateToken(string refreshJti, string newAuthJti, string newRefreshJti, DateTime updated);
    }
}
