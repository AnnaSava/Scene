using AutoMapper;
using SavaDev.Base.Data.Models;
using SavaDev.Base.Data.Registry.Filter;
using SavaDev.Base.Front.Services;
using SavaDev.Base.Users.Security;
using SavaDev.Scheme.Contract.Models;
using SavaDev.Scheme.Data.Contract;
using SavaDev.Scheme.Data.Contract.Enums;
using SavaDev.Scheme.Data.Contract.Models;
using SavaDev.Scheme.Front.Contract;
using SavaDev.Scheme.Front.Contract.Models;
using System.Text.Json;

namespace SavaDev.Scheme.Front.Services
{
    public class RegistryViewService : IRegistryViewService
    {
        protected readonly IRegistryService _registryService;
        protected readonly IFilterService _filterService;
        protected readonly IColumnService _columnService;
        protected readonly IRegistryConfigService _columnConfigService;
        protected readonly ISecurityService _securityService;
        protected readonly IMapper _mapper;

        public RegistryViewService(IRegistryService tableService,
            IFilterService filterService,
            IColumnService columnService,
            IRegistryConfigService columnConfigService,
            ISecurityService securityService,
            IMapper mapper) 
        {
            _registryService= tableService;
            _columnConfigService= columnConfigService;
            _columnService= columnService;
            _filterService= filterService;
            _securityService = securityService;
            _mapper = mapper;
        }

        public async Task<RegistryViewModel> GetOne(ModelPlacement placement)
        {
            var model = await _registryService.GetOneByPlacement(placement);
            if (model == null)
                throw new Exception($"Model with placement {placement.Module} {placement.Entity} not found.");
            var vm = _mapper.Map<RegistryViewModel>(model);

            var filters = await _filterService.GetAll(model.Id);
            vm.Filters = _mapper.Map<IEnumerable<FilterViewModel>>(filters).ToList();
            var lastFilter = await _filterService.GetLast(model.Id);
            await ApplyFilter(lastFilter, vm);

            var configs = await _columnConfigService.GetAll(model.Id);
            vm.Configs = _mapper.Map<IEnumerable<RegistryConfigViewModel>>(configs).ToList();
            var lastConfig = await _columnConfigService.GetLast(model.Id);
            await ApplyConfig(lastConfig, vm);            

            return vm;
        }

        public async Task<List<FilterViewModel>> GetAllFilters(Guid registryId)
        {
            var filters = await _filterService.GetAll(registryId);
            var vm = _mapper.Map<IEnumerable<FilterViewModel>>(filters).ToList();
            return vm;
        }

        public async Task ApplyFilter(long filterId, RegistryViewModel vm)
        {
            var filter = await _filterService.GetOne(filterId);
            await ApplyFilter(filter, vm);
        }

        private async Task ApplyFilter(FilterModel filter, RegistryViewModel vm)
        {
            if (filter != null)
            {
                vm.SelectedFilter = _mapper.Map<FilterViewModel>(filter);
            }
        }

        public async Task ApplyConfig(long configId, RegistryViewModel vm)
        {
            var config = await _columnConfigService.GetOne(configId);
            await ApplyConfig(config, vm);
        }

        private async Task ApplyConfig(RegistryConfigModel config, RegistryViewModel vm)
        {
            var columns = await _columnService.GetAll(vm.Id);
            var colVms = columns.Select(m => _mapper.Map<ColumnViewModel>(m));

            if (config != null)
            {
                vm.DisplayedColumns.Clear();

                var idsArr = config.Columns.Split(',').Select(m => Guid.Parse(m));

                foreach (var id in idsArr)
                {
                    var col = colVms.FirstOrDefault(m => m.Id == id);
                    if (col != null)
                    {
                        vm.DisplayedColumns.Add(_mapper.Map<ColumnViewModel>(col));
                    }
                }

                vm.SelectedConfig = _mapper.Map<RegistryConfigViewModel>(config);
                vm.AvailableColumns = colVms.Where(m => !idsArr.Contains(m.Id)).ToList();
            }
            else
            {
                vm.AvailableColumns = colVms.Where(m=>m.Display == ColumnDisplay.Available).ToList();
                vm.DisplayedColumns = colVms.Where(m=>m.Display == ColumnDisplay.Default).ToList();
            }
        }

        public async Task<OperationViewResult> SaveConfig(RegistryConfigViewModel model)
        {
            var newModel = _mapper.Map<RegistryConfigModel>(model);
            newModel.Columns = string.Join(',', model.ColumnIds);

            if(!model.ForAll)
            {
                newModel.OwnerId = _securityService.GetId();
            }

            var resultModel = model.Id == 0
                ? await _columnConfigService.Create(newModel)
                : await _columnConfigService.Update(model.Id, newModel);
            return new OperationViewResult(resultModel.Details);
        }

        public async Task<OperationViewResult> RemoveConfig(long id)
        {
            var result = await _columnConfigService.Remove(id);
            return new OperationViewResult(result.Details);
        }

        public async Task<RegistryConfigViewModel> GetLastConfig(Guid tableId)
        {
            var model = await _columnConfigService.GetLast(tableId);
            return _mapper.Map<RegistryConfigViewModel>(model);
        }

        public async Task<OperationViewResult> SaveFilter(FilterViewModel model, BaseFilter filter)
        {
            var newModel = _mapper.Map<FilterModel>(model);
            newModel.Fields = JsonSerializer.Serialize(filter);

            if (!model.ForAll)
            {
                newModel.OwnerId = _securityService.GetId();
            }

            var resultModel = await _filterService.Create(newModel);
            return new OperationViewResult(resultModel.Details);
        }

        public async Task<OperationViewResult> RemoveFilter(long id)
        {
            var result = await _filterService.Remove(id);
            return new OperationViewResult(result.Rows, result.ProcessedObject);
        }
    }
}
