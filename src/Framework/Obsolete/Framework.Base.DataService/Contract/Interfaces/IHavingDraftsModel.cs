using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Contract.Interfaces
{
    [Obsolete]
    public interface IHavingDraftsModel
    {
        Guid? DraftId { get; set; }
    }
}
