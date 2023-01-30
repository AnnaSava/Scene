using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.Types.Registry;
using Sava.Media.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Media.Data.Contract
{
    public interface IGalleryService
    {
        Task<OperationResult<GalleryModel>> Create(GalleryModel model);

        Task<GalleryModel> GetOne(Guid id);

        Task<PageListModel<GalleryModel>> GetAll(int page, int count);
    }
}
