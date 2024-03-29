﻿using SavaDev.Base.Data.Models;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
using SavaDev.General.Front.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.General.Front.Contract
{
    /// <summary>
    /// Service to work with reserved names.
    /// Reserved names are the names which user is not allowed to use as login.
    /// </summary>
    public interface IReservedNameViewService
    {
        Task<ReservedNameViewModel> GetOne(string text);

        Task<ReservedNameViewModel> Create(ReservedNameViewModel model);

        Task Remove(string text);

        Task<ReservedNameViewModel> Update(string text, ReservedNameFormViewModel model);

        Task<RegistryPageViewModel<ReservedNameViewModel>> GetRegistryPage(RegistryQuery query);

        Task<Dictionary<string, bool>> CheckExists(string text);

        Task<FieldValidationResult> ValidateField(string name, string value);
    }
}
