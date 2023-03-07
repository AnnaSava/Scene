using SavaDev.Base.Front.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Community.Front.Contract.Models
{
    public class GroupViewModel : BaseRegistryItemViewModel<Guid>
    {
        public string OwnerId { get; set; }

        public string AttachedToId { get; set; }

        public string Module { get; set; }

        public string Entity { get; set; }
    }
}
