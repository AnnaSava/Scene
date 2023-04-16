using Microsoft.EntityFrameworkCore.Query.Internal;
using Sava.Media.Contract;
using Sava.Media.Contract.Models;
using SavaDev.Base.Data.Services;
using SavaDev.Infrastructure.Util.ImageEditor;
using SavaDev.Media.Data.Contract;
using SavaDev.Media.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Media.Front.Managers
{
    public class UploadImageManager<TModel>
        where TModel : IHavingGalleryModel
    {
        private readonly IHavingGalleryEntityService _entityService;
        private readonly IGalleryProcessor _galleryProcessor;
        private readonly string UserId;
        private readonly string Module;

        public UploadImageManager(string module, string userId, IHavingGalleryEntityService entityService, IGalleryProcessor galleryProcessor)
        {
            Module = module;
            UserId = userId;
            _entityService = entityService;
            _galleryProcessor = galleryProcessor;
        }

        public async Task<OperationResult> UploadImage(long id, Stream stream)
        {
            var model = await _entityService.GetOne<TModel>(id);

            var imgModel = new ImageSavingModel
            {
                OwnerId = UserId,
                Module = Module,
                GalleryId = model.GalleryId,
            };

            _galleryProcessor.Configure(new ImageProcessorOptions
            {
                ImageResizeKinds = new string[] {
                    ImageResizeKindName.CenterMiddle3x1,
                    ImageResizeKindName.CenterThumb3x1,
                    ImageResizeKindName.CenterThumbMobile3x1
                },
                PreviewImageKind = ImageResizeKindName.CenterThumb3x1,
                PreviewMobileImageKind = ImageResizeKindName.CenterThumbMobile3x1,
            });
            var vm = await _galleryProcessor.SaveImage(stream, imgModel);
            if (!imgModel.GalleryId.HasValue)
            {
                var res = await _entityService.SetGalleryId(id, vm.GalleryId);
            }

            await _entityService.SetPreviewImageId(id, vm.Id);

            // TODO
            return new OperationResult(1);
        }
    }
}
