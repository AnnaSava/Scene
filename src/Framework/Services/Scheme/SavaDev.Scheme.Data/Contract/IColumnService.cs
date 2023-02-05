using SavaDev.Base.Data.Models;
using SavaDev.Base.Data.Services;
using SavaDev.Scheme.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Data.Contract
{
    public interface IColumnService
    {
        Task<OperationResult> Create(ColumnModel model);

        Task<IEnumerable<ColumnModel>> GetAll(ModelPlacement modelPlacement);
    }
}
