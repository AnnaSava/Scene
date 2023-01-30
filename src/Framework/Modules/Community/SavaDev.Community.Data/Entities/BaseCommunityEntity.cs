using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Community.Data.Entities
{
    public abstract class BaseCommunityEntity
    {
        public long Id { get; set; }

        public Guid CommunityId { get; set; }

        public virtual Community Community { get; set; }
    }
}
