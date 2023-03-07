using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
using SavaDev.Content.Contract.Models;

namespace SavaDev.Content.Contract
{
    public interface IVersionViewService
    {
        Task<RegistryPageViewModel<VersionViewModel>> GetRegistryPage(RegistryQuery query);
    }
}
