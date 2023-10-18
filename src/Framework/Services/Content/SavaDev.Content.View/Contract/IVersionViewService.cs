using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
using SavaDev.Content.View.Contract.Models;

namespace SavaDev.Content.View.Contract
{
    public interface IVersionViewService
    {
        Task<RegistryPageViewModel<VersionViewModel>> GetRegistryPage(RegistryQuery query);
    }
}
