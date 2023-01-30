using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Models
{
    public class UserLockoutModel
    {
        public long Id { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public string Reason { get; set; }
    }
}
