using AutoMapper;
using SavaDev.Content.Contract;
using SavaDev.Content.Contract.Models;
using SavaDev.Content.Data;
using SavaDev.Content.Data.Contract;
using SavaDev.Content.Data.Contract.Models;
using SavaDev.Base.Front.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Services;

namespace SavaDev.Content.Services
{
    public class VersionFrontService : IVersionFrontService
    {
        private readonly IVersionService _versionService;
        private readonly IMapper _mapper;

        public VersionFrontService(IVersionService versionService, IMapper mapper)
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
