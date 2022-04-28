using Framework.Base.DataService.Entities;
using Framework.Base.Types.ModelTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Contract.Interfaces
{
    public interface IAliasedEntityService<TModel> : IRestorableEntityService<TModel>
         where TModel : IModelRestorable, IModelAliased
    {
        Task<TModel> GetOneByAlias(string alias);
    }
}
