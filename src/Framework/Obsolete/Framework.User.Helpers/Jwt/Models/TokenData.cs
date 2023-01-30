using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.User.Helpers.Jwt.Models
{
    public class TokenData
    {
        public string Jti { get; }

        public string Token { get; set; }

        public TokenData()
        {
            Jti = CreateJti();
        }

        public override string ToString()
        {
            return Token;
        }

        private string CreateJti()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            return GuidString;
        }
    }
}
