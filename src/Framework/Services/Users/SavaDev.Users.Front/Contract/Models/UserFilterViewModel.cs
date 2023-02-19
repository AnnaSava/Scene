using Framework.Base.Service.ListView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Users.Front.Contract.Models
{
    public class UserFilterViewModel : ListFilterViewModel
    {
        public string Login { get; set; }

        public string Email { get; set; }

        public bool? EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool? PhoneNumberConfirmed { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public bool? IsBanned { get; set; }
    }
}
