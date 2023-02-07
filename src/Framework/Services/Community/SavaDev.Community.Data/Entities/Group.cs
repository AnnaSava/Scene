using SavaDev.Base.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Community.Data.Entities
{
    public class Group : BaseHubEntity
    {
        public virtual ICollection<Lockout> Lockouts { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
