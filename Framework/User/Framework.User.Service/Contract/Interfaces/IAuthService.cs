using Framework.User.Service.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Contract.Interfaces
{
    public interface IAuthService
    {
        Task<AuthorizedViewModel> Authorize(AuthorizingViewModel model);

        Task<AuthorizedViewModel> RefreshToken(string authHeader);
    }
}
