using SavaDev.Files.Data.Contract.Models;
using SavaDev.Files.Service.Contract.Models;

namespace SavaDev.Files.Service.Contract
{
    public interface IFileProcessingService
    {
        Task<FileModel> UploadFilePreventDuplicate(FilesDataModel model);
    }
}
