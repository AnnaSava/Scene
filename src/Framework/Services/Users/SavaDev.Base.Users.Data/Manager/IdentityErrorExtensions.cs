using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Users.Data.Manager
{
    public static class IdentityErrorExtensions
    {
        public static string GetString(this IEnumerable<IdentityError> list)
        {
            var strings = list.Select(m => $"{m.Code}: {m.Description}");
            return string.Join('\n', strings);
        }
    }
}
