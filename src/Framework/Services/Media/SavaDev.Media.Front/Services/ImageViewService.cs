using AutoMapper;
using Sava.Media.Contract;
using Sava.Media.Contract.Models;
using Sava.Media.Data.Contract;
using Sava.Media.Data.Contract.Models;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
using SavaDev.Base.Front.Services;
using SavaDev.Base.Users.Security;
using SavaDev.Files.Data.Contract.Models;
using SavaDev.Files.Service.Contract;
using SavaDev.Files.Service.Contract.Models;
using SavaDev.Infrastructure.Util.ImageEditor;
using SavaDev.Media.Data.Contract.Models;

namespace Sava.Media.Services
{
    public class ImageViewService : IImageViewService
    {
        private readonly IImageService _imageService;
        private readonly IGalleryService _galleryService;
        private readonly IFilesUploader _fileUploader;
        private readonly ImageEditor _imageEditor;
        private readonly IUserProvider _userProvider;
        private readonly IMapper _mapper;

        private readonly Dictionary<string, (int, int, bool)> ImageKinds;

        public ImageViewService(IImageService imageService,
            IGalleryService galleryService,
            ImageEditor imageEditor,
            IFilesUploader fileUploader,
            IUserProvider userProvider,
            IMapper mapper)
        {
            _imageService = imageService;
            _galleryService = galleryService;
            _imageEditor = imageEditor;
            _fileUploader = fileUploader;
            _userProvider = userProvider;
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

            var uploadedFileModel = await _fileUploader.SendInfo(new FilesDataModel(content, _userProvider.UserId));
            storedImageFiles.Add("Original", uploadedFileModel.SavedFile);

            var resizedFileModel = await ResizeAndSave(uploadedFileModel.SavedFile);
            storedImageFiles.Add("Resized", resizedFileModel.SavedFile);

            var thumbFileModel = await CropAndSave(resizedFileModel.SavedFile);
            storedImageFiles.Add("Thumb", thumbFileModel.SavedFile);

            var imageModel = new ImageFormModel
            {
                PreviewId = thumbFileModel.SavedFile.Id.ToString(),
                Files = new List<ImageFileFormModel>(),
                OwnerId = _userProvider.UserId
            };

            foreach (var image in storedImageFiles)
            {
                var size = _imageEditor.GetSize(image.Value.Content);

                var imgFile = new ImageFileFormModel
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
                var result = await _galleryService.Create(new GalleryModel { OwnerId = _userProvider.UserId, AttachedToId = "", Module = "", Entity = "" });
                if (result == null)
                    throw new Exception("Error creating gallery");
                imageModel.GalleryId = result.GetProcessedObject<GalleryModel>().Id;
            }

            var createdImage = await _imageService.Create(imageModel);
            return _mapper.Map<ImageViewModel>(createdImage);
        }

        private async Task<FilesUploadResult> ResizeAndSave(FileModel fileModel)
        {
            using var ms = new MemoryStream();
            _imageEditor.Resize(fileModel.Content, ms);

            var content = ms.ToArray();

            var saved = await _fileUploader.SendInfo(new FilesDataModel(content, _userProvider.UserId));
            return saved;
        }

        private async Task<FilesUploadResult> CropAndSave(FileModel fileModel)
        {
            using var ms = new MemoryStream();
            _imageEditor.SquareCrop(fileModel.Content, ms);

            var content = ms.ToArray();

            var saved = await _fileUploader.SendInfo(new FilesDataModel(content, _userProvider.UserId));
            return saved;
        }
    }
}
