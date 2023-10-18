using AutoMapper;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
using SavaDev.Base.Front.Services;
using SavaDev.Content.Data;
using SavaDev.Content.Data.Contract;
using SavaDev.Content.Data.Contract.Models;
using SavaDev.Content.View.Contract;
using SavaDev.Content.View.Contract.Models;

namespace SavaDev.Content.View.Services
{
    public class VersionViewService : IVersionViewService
    {
        private readonly IVersionService _versionService;
        private readonly IMapper _mapper;

        public VersionViewService(IVersionService versionService, IMapper mapper)
        {
            _versionService = versionService;
            _mapper = mapper;
        }

        public async Task<RegistryPageViewModel<VersionViewModel>> GetRegistryPage(RegistryQuery query)
        {
            var manager = new RegistryPageManager<VersionModel, VersionFilterModel>(_versionService, _mapper);
            var vm = await manager.GetRegistryPage<VersionViewModel>(query);
            return vm;
        }
    }
}
