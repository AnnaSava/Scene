using SavaDev.Base.Data.Models.Interfaces;
using SavaDev.Base.Data.Services;
using SavaDev.Base.Data.Services.Interfaces;
using SavaDev.Base.Front.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Content.View.Managers
{
    public class RestoreContentManager<TKey, TFormModel>
    {
        protected readonly IEntityEditService<TKey, TFormModel> _entityService;

        public Func<TKey, Task<bool>>? CheckAccess { get; set; }

        public Func<TFormModel, Task<OperationResult>>? ProcessVersion { get; set; }

        public RestoreContentManager(IEntityEditService<TKey, TFormModel> entityService)
        {
            _entityService = entityService;
        }

        public async Task<OperationResult> Restore(TKey id)
        {
            if (CheckAccess == null)
            {
                throw new NotPermittedException();
            }
            var can = await CheckAccess.Invoke(id);
            if (!can)
            {
                throw new NotPermittedException();
            }

            var result = await _entityService.Restore(id);

            if (!result.IsSuccess)
            {
                throw new Exception($"Restoring an object id {id} failed");
            }

            return result;
        }
    }
}
