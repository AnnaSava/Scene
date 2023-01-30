using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Entities
{
    public class Lockout
    {
        [Key]
        public long Id { get; set; }

        public long UserId { get; set; }

        public long LockedByUserId { get; set; }

        public DateTime LockDate { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public string Reason { get; set; }
    }
}
