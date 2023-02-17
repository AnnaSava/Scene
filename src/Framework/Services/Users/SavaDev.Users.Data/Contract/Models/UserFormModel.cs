using SavaDev.Base.Data.Models.Interfaces;
using SavaDev.Base.User.Data.Models;
using SavaDev.Base.User.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Users.Data.Contract.Models
{
    public class UserFormModel : BaseUserModel, IUserModel, IModel<long>
    {
        public long Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public DateTime? Birthday { get; set; }

        public string AvatarPath { get; set; }
    }
}
