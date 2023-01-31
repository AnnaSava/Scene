using SavaDev.Base.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Community.Data.Entities
{
    public class Lockout : BaseCommunityEntity, IAnyEntity
    {
        public string UserId { get; set; }

        public string LockedByUserId { get; set; }

        public DateTime LockDate { get; set; }

        public DateTime? LockoutEnd { get; set; }

        public string Reason { get; set; }
    }
}
