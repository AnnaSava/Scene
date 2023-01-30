using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Services
{
    internal class UserInfoCacheModel
    {
        public bool? IsLocked { get; set; }

        public IEnumerable<string> Permissions { get; set; }
    }
}
