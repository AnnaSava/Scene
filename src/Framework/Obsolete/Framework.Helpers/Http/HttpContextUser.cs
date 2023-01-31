using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Helpers.Http
{
    [Obsolete]
    public static class HttpContextUser
    {
        public static string GetId(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext.User;
            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
                return null;
            var idClaim = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (idClaim == null) return null;

            return idClaim.Value;
        }

        public static long GetLongId(IHttpContextAccessor httpContextAccessor)
        {
            var strId = GetId(httpContextAccessor);
            if (string.IsNullOrEmpty(strId)) return 0;

            if (long.TryParse(strId, out long longId))
                return longId;

            return 0;
        }
    }
}
