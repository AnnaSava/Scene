using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Models
{
    public class UserLockoutModel
    {
        public long Id { get; set; }

        public DateTime? LockoutEnd { get; set; }

        public string Reason { get; set; }
    }
}
