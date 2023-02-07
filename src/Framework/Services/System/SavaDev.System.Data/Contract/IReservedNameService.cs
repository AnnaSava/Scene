using SavaDev.Base.Data.Services;
using SavaDev.System.Data.Contract.Models;

namespace SavaDev.System.Data.Contract
{
    public interface IReservedNameService
    {
        Task<OperationResult> Create(ReservedNameModel model);

        Task<OperationResult> Update(ReservedNameModel model);

        Task<OperationResult> Remove(string text);
        
        Task<ReservedNameModel> GetOne(string text);

        //Task<PageListModel<ReservedNameModel>> GetAll(ListQueryModel<ReservedNameFilterModel> query);

        /// <summary>
        /// Checks if the name is reserved
        /// </summary>
        /// <param name="text">Text of the name</param>
        /// <returns>True if reserved</returns>
        Task<bool> CheckIsReserved(string text);

        Task<bool> CheckExists(string text);
    }
}
