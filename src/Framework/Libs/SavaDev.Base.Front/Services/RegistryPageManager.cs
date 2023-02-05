﻿using AutoMapper;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Registry.Filter;
using SavaDev.Base.Data.Services;
using SavaDev.Base.Front.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Front.Services
{
    public class RegistryPageManager<TItemModel, TFilterModel>
        where TFilterModel : BaseFilter
    {
        protected readonly IEntityRegistryService<TItemModel, TFilterModel> _entityService;
        protected readonly IMapper _mapper;

        public RegistryPageManager(IEntityRegistryService<TItemModel, TFilterModel> entityService,
            IMapper mapper)
        {
            _entityService = entityService;
            _mapper = mapper;
        }

        public async Task<RegistryPageViewModel<TItemViewModel>> GetRegistryPage<TItemViewModel, TFilterViewModel>(RegistryQuery<TFilterViewModel> query)
        {
            var filter = _mapper.Map<TFilterModel>(query.Filter);

            var queryModel = new RegistryQuery<TFilterModel>(query.PageInfo, query.Sort)
            {
                Filter = filter
            };

            var page = await _entityService.GetRegistryPage(queryModel);

            try
            {
                var vm = RegistryPageMapper.MapRegistry<TItemModel, TItemViewModel>(page, _mapper);
                return vm;
            }
            catch (Exception ex)
            {

            }
            return default;
        }
    }
}
