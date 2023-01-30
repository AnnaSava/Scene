using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Community.Data.Entities
{
    public class UserRole
    {
        public string UserId { get; set; }

        public long RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
