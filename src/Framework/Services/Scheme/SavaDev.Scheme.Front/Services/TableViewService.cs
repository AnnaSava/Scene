using AutoMapper;
using SavaDev.Base.Data.Models;
using SavaDev.Base.Data.Registry.Filter;
using SavaDev.Base.Front.Services;
using SavaDev.Base.Users.Security;
using SavaDev.Scheme.Contract.Models;
using SavaDev.Scheme.Data.Contract;
using SavaDev.Scheme.Data.Contract.Models;
using SavaDev.Scheme.Front.Contract;
using SavaDev.Scheme.Front.Contract.Models;
using System.Text.Json;

namespace SavaDev.Scheme.Front.Services
{
    public class TableViewService : ITableViewService
    {
        protected readonly ITableService _tableService;
        protected readonly IFilterService _filterService;
        protected readonly IColumnService _columnService;
        protected readonly IColumnConfigService _columnConfigService;
        protected readonly ISecurityService _securityService;
        protected readonly IMapper _mapper;

        public TableViewService(ITableService tableService,
            IFilterService filterService,
            IColumnService columnService,
            IColumnConfigService columnConfigService,
            ISecurityService securityService,
            IMapper mapper) 
        {
            _tableService= tableService;
            _columnConfigService= columnConfigService;
            _columnService= columnService;
            _filterService= filterService;
            _securityService = securityService;
            _mapper = mapper;
        }

        public async Task<TableViewModel> GetOne(ModelPlacement placement)
        {
            var model = await _tableService.GetOneByPlacement(placement);
            var vm = _mapper.Map<TableViewModel>(model);

            var filters = await _filterService.GetAll(model.Id);
            vm.Filters = _mapper.Map<IEnumerable<FilterViewModel>>(filters).ToList();

            var columnConfig = await _columnConfigService.GetLast(model.Id);
            var c = await _columnService.GetAll(model.Id);
            var cols = c.Select(m => _mapper.Map<ColumnViewModel>(m));

            //await FillColumns(vm, columnConfig.Id);

            if (columnConfig != null)
            {
                var idsArr = columnConfig.Columns.Split(',').Select(m => Guid.Parse(m));

                foreach (var id in idsArr)
                {
                    var col = cols.FirstOrDefault(m => m.Id == id);
                    if (col != null)
                    {
                        vm.DisplayedColumns.Add(_mapper.Map<ColumnViewModel>(col));
                    }
                }

                vm.AvailableColumns = cols.Where(m => !idsArr.Contains(m.Id)).ToList();
            }
            else
            {
                vm.DisplayedColumns = cols.ToList();
            }

            var configs = await _columnConfigService.GetAll(model.Id);
            vm.ColumnConfigs = configs.Select(m => _mapper.Map<ColumnConfigViewModel>(m)).ToList();
            vm.SelectedConfig = _mapper.Map<ColumnConfigViewModel>(columnConfig);
            return vm;
        }

        public async Task FillColumns(TableViewModel vm, long configId)
        {
            var config = vm.ColumnConfigs.FirstOrDefault(m => m.Id == configId);
            vm.AvailableColumns.Clear();
            vm.DisplayedColumns.Clear();

            var c = await _columnService.GetAll(vm.Id);
            var cols = c.Select(m => _mapper.Map<ColumnViewModel>(m));

            if (config != null)
            {
                var idsArr = config.Columns.Split(',').Select(m => Guid.Parse(m));

                foreach (var id in idsArr)
                {
                    var col = cols.FirstOrDefault(m => m.Id == id);
                    if (col != null)
                    {
                        vm.DisplayedColumns.Add(_mapper.Map<ColumnViewModel>(col));
                    }
                }

                vm.AvailableColumns = cols.Where(m => !idsArr.Contains(m.Id)).ToList();
            }
            else
            {
                vm.DisplayedColumns = cols.ToList();
            }
        }


        public async Task<OperationViewResult> CreateConfig(ColumnConfigViewModel model)
        {
            var newModel = _mapper.Map<ColumnConfigModel>(model);
            newModel.Columns = string.Join(',', model.ColumnIds);

            if(!model.ForAll)
            {
                newModel.OwnerId = _securityService.GetId();
            }

            var resultModel = await _columnConfigService.Create(newModel);
            return new OperationViewResult(resultModel.Details);
        }

        public async Task<OperationViewResult> RemoveConfig(long id)
        {
            var result = await _columnConfigService.Remove(id);
            return new OperationViewResult(result.Details);
        }

        public async Task<ColumnConfigViewModel> GetLastConfig(Guid tableId)
        {
            var model = await _columnConfigService.GetLast(tableId);
            return _mapper.Map<ColumnConfigViewModel>(model);
        }

        public async Task<OperationViewResult> CreateFilter(FilterViewModel model, BaseFilter filter)
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
