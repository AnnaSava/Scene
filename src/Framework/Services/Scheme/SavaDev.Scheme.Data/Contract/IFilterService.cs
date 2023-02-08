using SavaDev.Base.Data.Services;
using SavaDev.Scheme.Contract.Models;
using SavaDev.Scheme.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Data.Contract
{
    public interface IFilterService
    {
        Task<OperationResult> Create(FilterModel model);

        Task<OperationResult> Remove(long id);

        Task<IEnumerable<FilterModel>> GetAll(Guid tableId);
    }
}
