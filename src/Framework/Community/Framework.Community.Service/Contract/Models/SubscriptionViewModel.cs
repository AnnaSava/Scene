using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Community.Service.Contract.Models
{
    public class SubscriptionViewModel
    {
        public long Id { get; set; }

        public string Module { get; set; }

        public string Entity { get; set; }

        public string CommunityId { get; set; }

        public string UserId { get; set; }

        public bool IsApprovedByOwner { get; set; }

        public bool IsApprovedByUser { get; set; }

        public bool IsLocked { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }
    }
}
