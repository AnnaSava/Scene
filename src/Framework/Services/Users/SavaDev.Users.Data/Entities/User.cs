using SavaDev.Base.Data.Entities.Interfaces;
using SavaDev.Base.User.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Users.Data
{
    public class User : BaseUser, IEntity<long>
    {
        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string MiddleName { get; set; } = "";

        public string DisplayName { get; set; } = "";
    }
}
