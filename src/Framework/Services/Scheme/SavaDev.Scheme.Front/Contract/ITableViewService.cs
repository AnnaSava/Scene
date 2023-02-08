using SavaDev.Base.Data.Models;
using SavaDev.Base.Data.Registry.Filter;
using SavaDev.Base.Data.Services;
using SavaDev.Scheme.Front.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Front.Contract
{
    public interface ITableViewService
    {
        Task<TableViewModel> GetOne(ModelPlacement placement);

        Task FillColumns(TableViewModel vm, long configId);

        Task<OperationResult> CreateFilter(FilterViewModel model, BaseFilter filter);

        Task<OperationResult> CreateConfig(ColumnConfigViewModel model);

        Task<ColumnConfigViewModel> GetLastConfig(Guid tableId);
    }
}
