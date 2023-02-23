using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Users.Data.Models.Interfaces
{
    public interface IUserFormModel
    {
        public long Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        //public string FirstName { get; set; }

        //public string LastName { get; set; }

        //public string DisplayName { get; set; }
    }
}
