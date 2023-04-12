using SavaDev.Files.Service.Contract;
using SavaDev.Files.Service.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Files.Service.Services
{
    public class FilesDirectUploader : IFilesUploader
    {
        private readonly IFileProcessingService _fileProcessingService;

        public FilesDirectUploader(IFileProcessingService fileProcessingService)
        {
            _fileProcessingService = fileProcessingService;
        }

        public async Task<FilesUploadResult> SendInfo(FilesDataModel info)
        {
            var resultModel = await _fileProcessingService.UploadFilePreventDuplicate(info.Content);

            return new FilesUploadResult { SavedFile = resultModel };
        }
    }
}
