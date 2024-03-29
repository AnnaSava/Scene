﻿using SavaDev.Files.Data.Contract;
using SavaDev.Files.Data.Contract.Models;
using SavaDev.Files.Service.Contract;
using SavaDev.Files.Service.Contract.Models;
using SavaDev.Infrastructure.Util.ImageEditor;

namespace SavaDev.Files.Service.Services
{
    public class FileProcessingService : IFileProcessingService
    {
        private readonly IFileService _fileRepository;
        private readonly MimeTypeChecker _mimeTypeChecker;
        private readonly HashHelper _hashHelper;

        public FileProcessingService(IFileService fileRepository,
            MimeTypeChecker mimeTypeChecker,
            HashHelper hashHelper)
        {
            _fileRepository = fileRepository;
            _mimeTypeChecker = mimeTypeChecker;
            _hashHelper = hashHelper;
        }

        public async Task<FileModel> UploadFilePreventDuplicate(FilesDataModel model)
        {
            var fileModel = MakeFileModelToUpload(model);

#if !DEBUG

            // Checking for duplicates, step 1
            fileModel.IsDuplicateMd5 = await _fileRepository.AnyByMd5(fileModel.Md5);

            FileModel duplicateFileModel = null;

            if (fileModel.IsDuplicateMd5)
            {
                // Checking for duplicates, step 2
                var duplicateMda5Files = await _fileRepository.GetAllByMd5(fileModel.Md5);

                foreach (var df in duplicateMda5Files)
                {
                    if (fileModel.Sha1 == df.Sha1)
                    {
                        fileModel.IsDuplicateSha1 = true;
                        duplicateFileModel = df;
                        //_logger.Log($"duplicate file {filePath}\n");
                        break;
                    }
                }
            }

            if (fileModel.IsDuplicateSha1)
                return duplicateFileModel;
#endif
            var uploadedFile = await _fileRepository.Create(fileModel);

            return uploadedFile;
        }

        private FileModel MakeFileModelToUpload(FilesDataModel filesModel)
        {
            var content = filesModel.Content;
            var mimeType = _mimeTypeChecker.GetMimeType(content);

            var model = new FileModel
            {
                Content = content,
                MimeType = mimeType,
                Md5 = _hashHelper.GetMd5Hash(content),
                Sha1 = _hashHelper.GetSha1Hash(content),
                Ext = _mimeTypeChecker.GetExtention(mimeType),
                Size = content.Length,
                DateUploaded = DateTime.UtcNow,
                // TODO Name наверно сделать nullable
                Name = "",
                Owner = filesModel.OwnerId
            };

            return model;
        }
    }
}
