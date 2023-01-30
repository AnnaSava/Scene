using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Community.Service.Contract.Models
{
    public class SubscriptionFormViewModel
    {
        public long Id { get; set; }

        public Guid CommunityId { get; set; }

        public string UserId { get; set; }

        public bool IsApprovedByOwner { get; set; }

        public bool IsApprovedByUser { get; set; }
    }
}
