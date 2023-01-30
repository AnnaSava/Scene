using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Contract.Interfaces
{
    public interface IAnyEntityService<TModel>
    {
        Task<TModel> Create(TModel model);
    }
}
