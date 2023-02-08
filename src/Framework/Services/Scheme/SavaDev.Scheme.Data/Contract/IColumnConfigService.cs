using SavaDev.Base.Data.Services;
using SavaDev.Scheme.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Data.Contract
{
    public interface IColumnConfigService
    {
        Task<OperationResult> Create(ColumnConfigModel model);

        Task<OperationResult> Remove(long id);

        Task<ColumnConfigModel> GetLast(Guid tableId);

        Task<IEnumerable<ColumnConfigModel>> GetAll(Guid tableId);
    }
}
