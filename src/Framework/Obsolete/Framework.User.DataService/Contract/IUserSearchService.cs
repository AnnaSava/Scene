using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract
{
    public interface IUserSearchService<TUserModel>
    {
        Task<IEnumerable<TUserModel>> GetAllByIds(IEnumerable<string> ids);
    }
}
