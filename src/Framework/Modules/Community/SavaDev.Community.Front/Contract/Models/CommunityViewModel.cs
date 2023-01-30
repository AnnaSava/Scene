using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Community.Service.Contract.Models
{
    public class CommunityViewModel
    {
        public Guid Id { get; set; }

        public string OwnerId { get; set; }

        public string AttachedToId { get; set; }

        public string Module { get; set; }

        public string Entity { get; set; }
    }
}
