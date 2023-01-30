using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Community.Data.Entities
{
    public class RolePermission
    {
        public long RoleId { get; set; }

        public virtual Role Role { get; set; }

        public string Permission { get; set; }
    }
}
