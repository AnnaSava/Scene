using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;
using SavaDev.Community.Data.Contract.Models;

namespace SavaDev.Community.Service.Contract
{
    public interface ICommunityService
    {
        Task<OperationResult<CommunityModel>> Create(CommunityModel model);

        Task<CommunityModel> GetOne(Guid id);

        Task<ItemsPage<CommunityModel>> GetAll(int page, int count);
    }
}
