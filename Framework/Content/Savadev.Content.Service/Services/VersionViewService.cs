using AutoMapper;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.Service.ListView;
using Savadev.Content.Contract;
using Savadev.Content.Contract.Models;
using Savadev.Content.Data;
using Savadev.Content.Data.Contract;
using Savadev.Content.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savadev.Content.Services
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

        public async Task<ListPageViewModel<VersionViewModel>> GetAll(VersionFilterViewModel filter, ListPageInfoViewModel pageInfo)
        {
            var filterModel = _mapper.Map<VersionFilterModel>(filter);

            var pageInfoModel = _mapper.Map<PageInfoModel>(pageInfo);

            var list = await _versionService.GetAll(new ListQueryModel<VersionFilterModel> { Filter = filterModel, PageInfo = pageInfoModel });

            var vm = ListPageViewModel.Map<VersionModel, VersionViewModel>(list, _mapper);
            return vm;
        }
    }
}
