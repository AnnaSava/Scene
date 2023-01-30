using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.Types.Registry;
using Framework.Community.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Community.Service.Contract
{
    public interface ICommunityService
    {
        Task<OperationResult<CommunityModel>> Create(CommunityModel model);

        Task<CommunityModel> GetOne(Guid id);

        Task<PageListModel<CommunityModel>> GetAll(int page, int count);
    }
}
