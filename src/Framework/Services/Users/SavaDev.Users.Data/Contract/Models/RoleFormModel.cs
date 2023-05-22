using SavaDev.Base.Users.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Users.Data.Contract.Models
{
    public class RoleFormModel : BaseRoleFormModel
    {
        public string Name { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }

        public IEnumerable<string> Permissions { get; set; }
    }
}
