using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Community.Data
{
    public abstract class BaseCommunityModel
    {
        public long Id { get; set; }

        public Guid CommunityId { get; set; }
    }
}
