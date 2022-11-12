﻿using Framework.Base.Service.ListView;
using Framework.User.Service.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Contract.Interfaces
{
    public interface IAppFrameworkRoleService
    {
        Task<AppRoleViewModel> GetOne(long id);

        Task<ListPageViewModel<AppRoleViewModel>> GetAll(AppRoleFilterViewModel filter, ListPageInfoViewModel pageInfo);

        Task<AppRoleViewModel> Create(AppRoleFormViewModel model);

        Task<AppRoleViewModel> Update(long id, AppRoleFormViewModel model);

        Task<AppRoleViewModel> Delete(long id);

        Task<AppRoleViewModel> Restore(long id);
    }
}