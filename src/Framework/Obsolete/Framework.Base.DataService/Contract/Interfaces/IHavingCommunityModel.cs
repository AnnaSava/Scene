using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Contract.Interfaces
{
    public interface IHavingCommunityModel<TKey>
    {
        public TKey Id { get; set; }

        public string OwnerId { get; set; }

        public Guid? CommunityId { get; set; }
    }
}
