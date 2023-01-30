using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Models
{
    public class UserRolesModel
    {
        public long UserId { get; set; }

        public ICollection<string> RoleNames { get; set; }
    }
}
