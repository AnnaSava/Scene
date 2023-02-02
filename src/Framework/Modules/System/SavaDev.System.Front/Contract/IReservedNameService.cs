using SavaDev.Base.Data.Models;
using SavaDev.System.Front.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.System.Front.Contract
{
    /// <summary>
    /// Service to work with reserved names.
    /// Reserved names are the names which user is not allowed to use as login.
    /// </summary>
    public interface IReservedNameService
    {
        Task<ReservedNameViewModel> GetOne(string text);

        Task<ReservedNameViewModel> Create(ReservedNameViewModel model);

        Task Remove(string text);

        Task<ReservedNameViewModel> Update(string text, ReservedNameFormViewModel model);

        //Task<ListPageViewModel<ReservedNameViewModel>> GetAll(ReservedNameFilterViewModel filter, ListPageInfoViewModel pageInfo);

        Task<Dictionary<string, bool>> CheckExists(string text);

        Task<FieldValidationResult> ValidateField(string name, string value);
    }
}
