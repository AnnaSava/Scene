﻿using AutoMapper;
using Sava.Media.Contract;
using Sava.Media.Contract.Models;
using Sava.Media.Data.Contract;
using Sava.Media.Data.Contract.Models;
using SavaDev.Base.Users.Security;
using SavaDev.Files.Data.Contract.Models;
using SavaDev.Files.Service.Contract;
using SavaDev.Infrastructure.Util.ImageEditor;

namespace Sava.Media.Services
{
    public class GalleryProcessor : IGalleryProcessor
    {
        private readonly IImageService _imageService;
        private readonly IGalleryService _galleryService;
        private readonly IFilesUploader _filesUploader;
        private readonly ImageEditor _imageEditor;
        private readonly IUserProvider _userProvider;
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
        private readonly Dictionary<string, ImageResizeOptions2> ImageKinds2;

        public GalleryProcessor(IImageService imageService,
            IGalleryService galleryService,
            ImageEditor imageEditor,
            IFilesUploader filesUploader,
            IUserProvider userProvider,
            IMapper mapper)
        {
            _imageService = imageService;
            _galleryService = galleryService;
            _imageEditor = imageEditor;
            _filesUploader = filesUploader;
            _userProvider = userProvider;
            _mapper = mapper;

            ImageKinds = new ImageResizeKinds().GetImageKinds();
            ImageKinds2 = new ImageResizeKinds().GetImageKinds2();
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

            var uploadedFileModel = await Upload(model.Content);            
            storedImageFiles.Add("Original", uploadedFileModel);

            foreach(var kind in options.ImageResizeKinds)
            {
                var resizedFileModel = await ResizeAndSave(uploadedFileModel, ImageKinds2[kind]);
                storedImageFiles.Add(kind, resizedFileModel);
            }

            //foreach (var kind in options.ImageResizeKinds)
            //{
            //    var resizedFileModel = await ResizeAndSave(uploadedFileModel, ImageKinds[kind]);
            //    storedImageFiles.Add(kind, resizedFileModel);
            //}

            var imageModel = new ImageFormModel
            {
                PreviewId = storedImageFiles[options.PreviewImageKind].Id.ToString(),
                Files = new List<ImageFileFormModel>(),
                OwnerId = model.OwnerId
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

            if (model.GalleryId.HasValue && model.GalleryId.Value != Guid.Empty)
            {
                imageModel.GalleryId = model.GalleryId.Value;
            }
            else
            {
                // TODO проперти галереи
                var result = await _galleryService.Create(new GalleryModel { OwnerId = model.OwnerId, AttachedToId = "", Entity = "", Module = "" });
                var galleryModel = result.GetProcessedObject<GalleryModel>();
                imageModel.GalleryId = galleryModel.Id;
            }

            var resultImage = await _imageService.Create(imageModel);
            var createdImage = resultImage.GetProcessedObject<ImageFormModel>();
            return _mapper.Map<ImageViewModel>(createdImage);
        }

        private async Task<FileModel> ResizeAndSave(FileModel fileModel, ImageResizeOptions2 options)
        {
            using var ms = new MemoryStream();
            _imageEditor.CropResize(fileModel.Content, ms, options);

            var content = ms.ToArray();

            var saved = await Upload(content);
            return saved;
        }

        private async Task<FileModel> Upload(byte[] content)
        {
            var uploadedFileModel = await _filesUploader.SendInfo(new SavaDev.Files.Service.Contract.Models.FilesDataModel(content, _userProvider.UserId));
            return uploadedFileModel.SavedFile;
        }
    }
}
