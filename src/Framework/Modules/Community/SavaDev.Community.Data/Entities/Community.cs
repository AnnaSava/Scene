using Framework.Base.DataService.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Community.Data.Entities
{
    public class Community : BaseHubEntity
    {
        public virtual ICollection<Lockout> Lockouts { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
