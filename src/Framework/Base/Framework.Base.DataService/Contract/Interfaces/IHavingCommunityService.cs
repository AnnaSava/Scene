using Framework.Base.Types.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Contract.Interfaces
{
    public interface IHavingCommunityService
    {
        Task<OperationResult> SetCommunityId(long id, Guid communityId);
    }

}
