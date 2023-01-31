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
    public class DraftViewService : IDraftViewService
    {
        private readonly IDraftService _draftService;
        private readonly IMapper _mapper;

        public DraftViewService(IDraftService draftService, IMapper mapper)
        {
            _draftService = draftService;
            _mapper = mapper;
        }

        //public async Task<ListPageViewModel<DraftViewModel>> GetAll(DraftFilterViewModel filter, ListPageInfoViewModel pageInfo)
        //{
        //    var filterModel = _mapper.Map<DraftFilterModel>(filter);

        //    var pageInfoModel = _mapper.Map<PageInfoModel>(pageInfo);

        //    var list = await _draftService.GetAll(new ListQueryModel<DraftFilterModel> { Filter = filterModel, PageInfo = pageInfoModel });

        //    var vm = ListPageViewModel.Map<DraftModel, DraftViewModel>(list, _mapper);
        //    return vm;
        //}
    }
}
