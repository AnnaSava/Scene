using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Models
{
    public class UserRolesModel
    {
        public long UserId { get; set; }

        public ICollection<string> RoleNames { get; set; }
    }
}
