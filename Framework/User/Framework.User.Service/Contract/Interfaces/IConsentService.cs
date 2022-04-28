using Framework.Base.Service.ListView;
using Framework.User.DataService.Contract.Models;
using Framework.User.Service.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Contract.Interfaces
{
    public interface IConsentService
    {
        Task<ConsentViewModel> GetOne(int id);

        Task<ConsentViewModel> Create(ConsentViewModel model);

        Task<ConsentViewModel> Update(ConsentViewModel model);

        Task<ConsentViewModel> Delete(int id);

        Task<ConsentViewModel> Restore(int id);

        Task<ListPageViewModel<ConsentViewModel>> GetAll(ConsentFilterViewModel filter, ListPageInfoViewModel pageInfo);
    }
}
