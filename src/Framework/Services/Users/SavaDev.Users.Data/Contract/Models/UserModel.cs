using SavaDev.Base.User.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Users.Data.Contract.Models
{
    public class UserModel : BaseUserModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public DateTime? Birthday { get; set; }

        public DateTime RegDate { get; set; }

        public string AvatarPath { get; set; }
    }
}
