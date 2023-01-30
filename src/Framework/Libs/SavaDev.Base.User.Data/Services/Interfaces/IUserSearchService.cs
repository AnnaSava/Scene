using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Services.Interfaces
{
    public interface IUserSearchService<TUserModel>
    {
        Task<IEnumerable<TUserModel>> GetAllByIds(IEnumerable<string> ids);
    }
}
