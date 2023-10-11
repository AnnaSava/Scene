using AutoMapper;
using SavaDev.Base.User.Data.Models;
using SavaDev.Content.Front.Contract.Enums;
using SavaDev.Content.Front.Contract.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SavaDev.Content.Front.Contract.Extensions
{
    public static class GetFormResultExtensions
    {
        public static TViewModel ToViewModel<TModel, TViewModel>(this GetFormViewResult result, TModel? contentModel, IMapper _mapper)
            where TModel : class, new()
            where TViewModel : IHavingDraftsFormViewModel
        {
            var form = new TModel();
            if (result.Action == FormActions.Update)
            {
                form = contentModel;
            }
            else if (!string.IsNullOrEmpty(result.Content))
            {
                form = JsonSerializer.Deserialize<TModel>(result.Content);
            }
            var vm = _mapper.Map<TViewModel>(form);
            vm.Drafts = result.Drafts;
            vm.HasMoreDrafts = result.HasMoreDrafts;
            return vm;
        }
    }
}
