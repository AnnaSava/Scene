using SavaDev.Files.Data.Contract.Models;

namespace Sava.Files.Contract
{
    public interface IFileProcessingService
    {
        Task<FileModel> UploadFilePreventDuplicate(byte[] content);
    }
}
