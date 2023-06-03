using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Community.Front.Contract.Models
{
    public class SubscriptionFormViewModel
    {
        public long Id { get; set; }

        public Guid GroupId { get; set; }

        public string UserId { get; set; }

        public bool IsApprovedByOwner { get; set; }

        public bool IsApprovedByUser { get; set; }
    }
}
