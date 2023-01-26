using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.Types.View;
using Framework.User.DataService.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Interfaces
{
    public interface IReservedNameDbService
    {
        Task<OperationResult<ReservedNameModel>> Create(ReservedNameModel model);

        Task<OperationResult<ReservedNameModel>> Update(ReservedNameModel model);

        Task<OperationResult> Remove(string text);
        
        Task<ReservedNameModel> GetOne(string text);

        Task<PageListModel<ReservedNameModel>> GetAll(ListQueryModel<ReservedNameFilterModel> query);

        /// <summary>
        /// Checks if the name is reserved
        /// </summary>
        /// <param name="text">Text of the name</param>
        /// <returns>True if reserved</returns>
        Task<bool> CheckIsReserved(string text);

        Task<bool> CheckExists(string text);
    }
}
