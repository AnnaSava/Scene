using Framework.Base.DataService.Contract.Models.ListView;
using Sava.Media.Data.Contract.Models;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;
using SavaDev.Base.Data.Services.Interfaces;
using SavaDev.Media.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Media.Data.Contract
{
    public interface IGalleryService : IEntityRegistryService<GalleryModel, GalleryFilterModel>
    {
        Task<OperationResult> Create(GalleryModel model);

        Task<GalleryModel> GetOne<GalleryModel>(Guid id);
    }
}
