using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Helpers.Jwt.Models
{
    public class CredentialsModel
    {
        public string nameid { get; set; }

        public string jti { get; set; }
    }
}
