using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Services.Interfaces
{
    public interface IHavingGroupService
    {
        Task<OperationResult> SetGroupId(long id, Guid communityId);
    }
}
