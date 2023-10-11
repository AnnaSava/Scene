﻿using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
using SavaDev.Base.Front.Services;
using SavaDev.Content.Contract.Models;
using SavaDev.Content.Front.Contract.Models;

namespace SavaDev.Content.Contract
{
    public interface IDraftViewService
    {
        const string Name = "Draft";

        Task<RegistryPageViewModel<DraftViewModel>> GetRegistryPage(RegistryQuery query);

        Task<OperationViewResult> Create(DraftViewModel model);

        Task<GetFormViewResult> GetForm(GetFormViewQuery query);
    }
}
