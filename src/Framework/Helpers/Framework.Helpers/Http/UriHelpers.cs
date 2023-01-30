using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Helpers.Http
{
    public static class UriHelpers
    {
        public static string MakeLoginRedirectUri(HttpRequest request, string loginUri)
        {
            var returnUri = new Uri(string.Format("{0}://{1}{2}{3}", request.Scheme, request.Host.Value, request.Path, request.QueryString));
            var uri = string.Format("{0}{1}", loginUri, returnUri);
            return uri;
        }
    }
}
