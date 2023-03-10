using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Services.Interfaces
{
    public interface IEntityCreateService<TFormModel>
    {
        Task<OperationResult> Create(TFormModel model);
    }

    public interface IEntityEditService<TKey, TFormModel>
    {
        Task<OperationResult> Update(TKey id, TFormModel model);

        Task<OperationResult> Delete(TKey id);

        Task<OperationResult> Restore(TKey id);
    }
}
