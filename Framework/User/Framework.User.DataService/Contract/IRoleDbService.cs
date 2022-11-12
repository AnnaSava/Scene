using Framework.Base.Types.ModelTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Interfaces
{
   public interface IRoleDbService<TModel> where TModel : IModel<long>
    {
        Task<TModel> GetOne(long id);

        Task<TModel> Create(TModel model);

        Task<TModel> Update(TModel model);

        Task<TModel> Delete(long id);

        Task<TModel> Restore(long id);
    }
}
