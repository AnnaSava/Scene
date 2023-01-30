using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Contract
{
    // TODO возможно, перенести в сборку DataService или вообще в отдельную сборку
    public interface ICurrentUserService
    {
        string GetId();
    }
}
