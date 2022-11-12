using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Interfaces
{
    public interface IUserSearchDbService<TUserModel, TFilterModel>
        where TFilterModel : ListFilterModel, new()
    {
        Task<PageListModel<TUserModel>> GetAll(ListQueryModel<TFilterModel> query);
    }
}
