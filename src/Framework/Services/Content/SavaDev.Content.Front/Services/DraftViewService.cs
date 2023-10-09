using AutoMapper;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
using SavaDev.Base.Front.Services;
using SavaDev.Content.Contract;
using SavaDev.Content.Contract.Models;
using SavaDev.Content.Data;
using SavaDev.Content.Data.Contract;
using SavaDev.Content.Data.Contract.Models;

namespace SavaDev.Content.Services
{
    public class DraftViewService : IDraftViewService
    {
        private readonly IDraftService _draftService;
        private readonly IMapper _mapper;

        public DraftViewService(IDraftService draftService, IMapper mapper)
        {
            _draftService = draftService;
            _mapper = mapper;
        }

        public async Task<RegistryPageViewModel<DraftViewModel>> GetRegistryPage(RegistryQuery query)
        {
            var manager = new RegistryPageManager<DraftModel, DraftFilterModel>(_draftService, _mapper);
            var vm = await manager.GetRegistryPage<DraftViewModel>(query);
            return vm;
        }

        public async Task<OperationViewResult> Create(DraftViewModel model)
        {
            return new OperationViewResult(12);
        }
    }
}
