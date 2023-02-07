using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Services.Interfaces
{
    public interface IEntityCrudService<TFormModel>
    {
        Task<OperationResult> Create(TFormModel model);

        Task<OperationResult> Update(long id, TFormModel model);

        Task<OperationResult> Delete(long id);

        Task<OperationResult> Restore(long id);
    }
}
