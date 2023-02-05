using AutoMapper;
using Framework.Base.Types.Image;
using Framework.Helpers.Files;
using Sava.Files.Contract;
using Sava.Media.Contract;
using Sava.Media.Contract.Models;
using Sava.Media.Data.Contract;
using Sava.Media.Data.Contract.Models;
using SavaDev.Files.Data.Contract.Models;

namespace Sava.Media.Services
{
    public class GalleryProcessor : IGalleryProcessor
    {
        private readonly IImageService _imageService;
        private readonly IGalleryService _galleryService;
        private readonly IFileProcessingService _fileProcessingService;
        private readonly ImageEditor _imageEditor;
        private readonly IMapper _mapper;

        private ImageProcessorOptions options = new ImageProcessorOptions { 
            ImageResizeKinds = new string[] 
            {
                ImageResizeKindName.Original,
                ImageResizeKindName.ResizedLarge,
                ImageResizeKindName.ResizedMiddle,
                ImageResizeKindName.Thumb,
                ImageResizeKindName.ThumbMobile
            },
            PreviewImageKind = ImageResizeKindName.Thumb,
            PreviewMobileImageKind = ImageResizeKindName.ThumbMobile
        };

        private readonly Dictionary<string, (int, int, bool)> ImageKinds;

        public GalleryProcessor(IImageService imageService,
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

        public void Configure(ImageProcessorOptions options)
        {
            this.options = options;
        }

        public async Task<ImageViewModel> SaveImage(Stream stream, ImageSavingModel model)
        {
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            model.Content = ms.ToArray();

            return await SaveImage(model);
        }

        public async Task<ImageViewModel> DownloadAndSaveImage(string fileUri, ImageSavingModel model)
        {
            using var client = new HttpClient();
            model.Content = await client.GetByteArrayAsync(fileUri);

            var saved = await SaveImage(model);

            return saved;
        }

        private async Task<ImageViewModel> SaveImage(ImageSavingModel model)
        {
            var storedImageFiles = new Dictionary<string, FileModel>();

            var uploadedFileModel = await _fileProcessingService.UploadFilePreventDuplicate(model.Content);
            storedImageFiles.Add("Original", uploadedFileModel);

            foreach (var kind in options.ImageResizeKinds)
            {
                var resizedFileModel = await ResizeAndSave(uploadedFileModel, ImageKinds[kind]);
                storedImageFiles.Add(kind, resizedFileModel);
            }

            var imageModel = new ImageModel
            {
                PreviewId = storedImageFiles[options.PreviewImageKind].Id.ToString(),
                Files = new List<ImageFileModel>(),
                OwnerId = model.OwnerId
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

            if (model.GalleryId.HasValue && model.GalleryId.Value != Guid.Empty)
            {
                imageModel.GalleryId = model.GalleryId.Value;
            }
            else
            {
                // TODO проперти галереи
                var createdGallery = await _galleryService.Create(new GalleryModel { OwnerId = model.OwnerId, AttachedToId = "" });
                imageModel.GalleryId = createdGallery.Models.First().Id;
            }

            var createdImage = await _imageService.Create(imageModel);
            return _mapper.Map<ImageViewModel>(createdImage);
        }

        private async Task<FileModel> ResizeAndSave(FileModel fileModel, (int, int, bool) options)
        {
            using var ms = new MemoryStream();
            _imageEditor.CropResize(fileModel.Content, ms, options.Item1, options.Item2, options.Item3);

            var content = ms.ToArray();

            var saved = await _fileProcessingService.UploadFilePreventDuplicate(content);
            return saved;
        }
    }
}
