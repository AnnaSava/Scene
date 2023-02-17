using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Entities
{
    public class Lockout
    {
        [Key]
        public long Id { get; set; }

        public long UserId { get; set; }

        public long LockedByUserId { get; set; }

        public DateTime LockDate { get; set; }

        public DateTime? LockoutEnd { get; set; }

        public string Reason { get; set; }
    }
}
