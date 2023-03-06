using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Users.Security
{
    public class HttpContextUserProvider : IUserProvider
    {
        public string? UserId { get; set; }

        public HttpContextUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            UserId = HttpContextUser.GetId(httpContextAccessor);
        }
    }
}
