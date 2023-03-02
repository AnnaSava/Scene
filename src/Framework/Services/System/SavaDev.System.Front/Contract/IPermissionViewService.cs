﻿using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
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
        Task<RegistryPageViewModel<PermissionViewModel>> GetRegistryPage(RegistryQuery query);

        Task<IEnumerable<PermissionTreeNodeViewModel>> GetTree();
    }
}
