using SavaDev.Base.Data.Models.Interfaces;
using SavaDev.Base.Data.Models;
using SavaDev.Base.Data.Services;
using SavaDev.Content.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavaDev.Content.Data.Contract;

namespace SavaDev.Content.Front.Managers
{
    public class VersionContentManager
    {
        private readonly IVersionService _versionService;
        private readonly string _userId;

        public VersionContentManager(IVersionService versionService, string userId)
        {
            _versionService = versionService;
            _userId = userId;
        }

        public async Task<OperationResult> CreateVersion<T>(T model)
            where T : IModel<long>
        {
            var placement = new ModelPlacement(typeof(T));

            var version = new VersionModel
            {
                ContentId = model.Id.ToString(),
                Entity = placement.Entity,
                Module = placement.Module,
                OwnerId = _userId
            };

            var res = await _versionService.Create(version, model);
            return res;
        }
    }
}
