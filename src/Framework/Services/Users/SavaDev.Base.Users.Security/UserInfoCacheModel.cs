using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Users.Security
{
    internal class UserInfoCacheModel
    {
        public bool? IsLocked { get; set; }

        public IEnumerable<string> Permissions { get; set; }
    }
}
