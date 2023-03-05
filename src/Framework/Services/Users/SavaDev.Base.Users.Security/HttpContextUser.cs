using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace SavaDev.Base.Users.Security
{
    public static class HttpContextUser
    {
        public static string GetId(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor == null)
                throw new ArgumentNullException("httpContextAccessor is null");
            if (httpContextAccessor.HttpContext == null)
                throw new ArgumentNullException("httpContextAccessor.HttpContext is null");

            var user = httpContextAccessor.HttpContext.User;
            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
                return null;
            var idClaim = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (idClaim == null) return null;

            return idClaim.Value;
        }

        public static string GetId(HttpContext httpContext)
        {
            var user = httpContext?.User;
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