using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Models
{
    public class LoginModel
    {
        public string LoginOrEmail { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
