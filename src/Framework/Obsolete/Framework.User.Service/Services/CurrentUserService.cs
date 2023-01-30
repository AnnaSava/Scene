using Framework.Helpers.Http;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.Service.Contract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Services
{
    // TODO возможно, объединить с секурити
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetId()
        {
            return HttpContextUser.GetId(_httpContextAccessor);
        }
    }
}
