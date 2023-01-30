using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Contract.Models
{
    public class UserLockoutViewModel
    {
        public long Id { get; set; }

        public string LockoutEnd { get; set; }

        public string Reason { get; set; }
    }
}
