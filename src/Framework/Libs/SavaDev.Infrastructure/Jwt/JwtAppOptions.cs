using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Jwt
{
    public class JwtAppOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key {get;set;}
        public int LifeTime{ get; set; } 
        public int RefreshLifeTime { get; set; }

        public TimeSpan GetLifeTime() => TimeSpan.FromMinutes(LifeTime);
        public TimeSpan GetRefreshLifeTime() => TimeSpan.FromDays(RefreshLifeTime);

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
