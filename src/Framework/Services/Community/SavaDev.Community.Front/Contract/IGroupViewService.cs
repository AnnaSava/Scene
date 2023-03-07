using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
using SavaDev.Community.Front.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Community.Front.Contract
{
    public interface IGroupViewService
    {
        Task<RegistryPageViewModel<GroupViewModel>> GetRegistryPage(RegistryQuery query);
    }
}
