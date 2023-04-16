using SavaDev.Base.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Media.Data.Contract
{
    public interface IHavingGalleryEntityService
    {
        Task<TModel> GetOne<TModel>(long id);

        Task<OperationResult> SetGalleryId(long id, Guid? galleryId);

        Task<OperationResult> SetPreviewImageId(long id, Guid? previewImageId);
    }
}
