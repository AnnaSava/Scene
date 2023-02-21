using AutoMapper;
using Framework.Base.Types.Image;
using Framework.Helpers.Files;
using Sava.Files.Contract;
using Sava.Media.Contract;
using Sava.Media.Contract.Models;
using Sava.Media.Data.Contract;
using Sava.Media.Data.Contract.Models;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
using SavaDev.Base.Front.Services;
using SavaDev.Files.Data.Contract.Models;
using SavaDev.Media.Data.Contract.Models;

namespace Sava.Media.Services
{
    public class ImageViewService : IImageViewService
    {
        private readonly IImageService _imageService;
        private readonly IGalleryService _galleryService;
        private readonly IFileProcessingService _fileProcessingService;
        private readonly ImageEditor _imageEditor;
        private readonly IMapper _mapper;

        private readonly Dictionary<string, (int, int, bool)> ImageKinds;

        public ImageViewService(IImageService imageService,
            IGalleryService galleryService,
            ImageEditor imageEditor,
            IFileProcessingService fileProcessingService,
            IMapper mapper)
        {
            _imageService = imageService;
            _galleryService = galleryService;
            _imageEditor = imageEditor;
            _fileProcessingService = fileProcessingService;
            _mapper = mapper;

            ImageKinds = new ImageResizeKinds().GetImageKinds();
        }

        public async Task<ImageViewModel> SaveImage(Stream stream, Guid? galleryId)
        {
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            var content = ms.ToArray();

            return await SaveImage(content, galleryId);
        }

        public async Task<ImageViewModel> DownloadAndSaveImage(string fileUri, Guid? galleryId)
        {
            using var client = new HttpClient();
            var response = await client.GetByteArrayAsync(fileUri);

            var saved = await SaveImage(response, galleryId);

            return saved;
        }

        public async Task<RegistryPageViewModel<ImageViewModel>> GetRegistryPage(RegistryQuery query)
        {
            var manager = new RegistryPageManager<ImageModel, ImageFilterModel>(_imageService, _mapper);
            var vm = await manager.GetRegistryPage<ImageViewModel>(query);
            return vm;
        }

        private async Task<ImageViewModel> SaveImage(byte[] content, Guid? galleryId)
        {
            var storedImageFiles = new Dictionary<string, FileModel>();

            var uploadedFileModel = await _fileProcessingService.UploadFilePreventDuplicate(content);
            storedImageFiles.Add("Original", uploadedFileModel);

            var resizedFileModel = await ResizeAndSave(uploadedFileModel);
            storedImageFiles.Add("Resized", resizedFileModel);

            var thumbFileModel = await CropAndSave(resizedFileModel);
            storedImageFiles.Add("Thumb", thumbFileModel);

            var imageModel = new ImageModel
            {
                PreviewId = thumbFileModel.Id.ToString(),
                Files = new List<ImageFileModel>(),
                // TODO пробрасывать через параметры
                OwnerId = ""
            };

            foreach (var image in storedImageFiles)
            {
                var size = _imageEditor.GetSize(image.Value.Content);

                var imgFile = new ImageFileModel
                {
                    FileId = image.Value.Id.ToString(),
                    Kind = image.Key,
                };

                imageModel.Files.Add(imgFile);
            }

            if (galleryId.HasValue && galleryId.Value != Guid.Empty)
            {
                imageModel.GalleryId = galleryId.Value;
            }
            else
            {
                // TODO проперти галереи
                var createdGallery = await _galleryService.Create(new GalleryModel { OwnerId = "", AttachedToId = "" });
               // imageModel.GalleryId = createdGallery.Models.First().Id;
            }

            var createdImage = await _imageService.Create(imageModel);
            return _mapper.Map<ImageViewModel>(createdImage);
        }

        private async Task<FileModel> ResizeAndSave(FileModel fileModel)
        {
            using var ms = new MemoryStream();
            _imageEditor.Resize(fileModel.Content, ms);

            var content = ms.ToArray();

            var saved = await _fileProcessingService.UploadFilePreventDuplicate(content);
            return saved;
        }

        private async Task<FileModel> CropAndSave(FileModel fileModel)
        {
            using var ms = new MemoryStream();
            _imageEditor.SquareCrop(fileModel.Content, ms);

            var content = ms.ToArray();

            var saved = await _fileProcessingService.UploadFilePreventDuplicate(content);
            return saved;
        }
    }
}
