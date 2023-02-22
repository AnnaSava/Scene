using SavaDev.Files.Data.Contract.Models;

namespace SavaDev.Files.Service.Contract
{
    public interface IFileProcessingService
    {
        Task<FileModel> UploadFilePreventDuplicate(byte[] content);
    }
}
