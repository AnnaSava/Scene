using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Models
{
    public class SignInResultModel<TUser> where TUser : BaseUserModel
    {
        public bool Succeeded { get; set; }

        public string PasswordHash { get; set; }

        public TUser User { get; set; }
    }
}
