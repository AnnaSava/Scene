using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Models;
using SavaDev.Base.Front.Registry;
using SavaDev.Base.Front.Services;
using SavaDev.Content.Contract.Models;
using SavaDev.Content.Front.Contract.Models;

namespace SavaDev.Content.Contract
{
    public interface IDraftFrontService
    {
        const string Name = "Draft";

        Task<ServiceCheckOk> Check(ServiceCheckQuery query);

        Task<RegistryPageViewModel<DraftViewModel>> GetRegistryPage(RegistryQuery query);

        Task<OperationViewResult> Create(DraftViewModel model);

        Task<GetFormViewResult> GetForm(GetFormViewQuery query);
    }
}
