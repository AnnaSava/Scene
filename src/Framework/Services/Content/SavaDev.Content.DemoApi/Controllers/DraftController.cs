using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Models;
using SavaDev.Base.Front.Registry;
using SavaDev.Base.Front.Services;
using SavaDev.Content.Contract;
using SavaDev.Content.Contract.Models;
using SavaDev.Content.Front.Contract.Models;

namespace SavaDev.Content.DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DraftController : ControllerBase, IDraftFrontService
    {
        private IDraftFrontService _draftService;

        public DraftController(IDraftFrontService draftService)
        {
            _draftService = draftService;
        }

        [HttpPost("[action]")]
        public async Task<ServiceCheckOk> Check(ServiceCheckQuery query)
            => await _draftService.Check(query);

        [HttpPost("[action]")]
        public async Task<RegistryPageViewModel<DraftViewModel>> GetRegistryPage(RegistryQuery query)
            => await _draftService.GetRegistryPage(query);

        [HttpPost("[action]")]
        public async Task<OperationViewResult> Create(DraftViewModel model)
            => await _draftService.Create(model);

        [HttpPost("[action]")]
        public async Task<GetFormViewResult> GetForm(GetFormViewQuery query)
            => await _draftService.GetForm(query);
    }
}
