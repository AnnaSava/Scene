using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
using SavaDev.Content.Contract.Models;

namespace SavaDev.Content.Contract
{
    public interface IDraftViewService
    {
        Task<RegistryPageViewModel<DraftViewModel>> GetRegistryPage(RegistryQuery query);
    }
}
