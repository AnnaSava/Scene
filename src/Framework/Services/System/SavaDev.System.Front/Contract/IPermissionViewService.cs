using SavaDev.System.Front.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.System.Front.Contract
{
    public interface IPermissionViewService
    {
        //Task<ListPageViewModel<PermissionViewModel>> GetAll(PermissionFilterViewModel filter, ListPageInfoViewModel pageInfo);

        Task<IEnumerable<PermissionTreeNodeViewModel>> GetTree();
    }
}
