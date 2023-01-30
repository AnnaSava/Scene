using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Community.Data.Contract
{
    public interface ILockoutService
    {
        Task<LockoutModel> Create(LockoutModel model);

        Task<IEnumerable<string>> GetAllActualIds(Guid communityId);
    }
}
