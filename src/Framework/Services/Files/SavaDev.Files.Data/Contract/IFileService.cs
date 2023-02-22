using Framework.Base.DataService.Contract.Models.ListView;
using SavaDev.Base.Data.Services.Interfaces;
using SavaDev.Files.Data.Contract.Models;

namespace SavaDev.Files.Data.Contract
{
    public interface IFileService : IEntityRegistryService<FileModel, FileFilterModel>
    {
        Task<FileModel> Create(FileModel model);

        Task<FileModel> GetOne(Guid id);

        Task<bool> AnyByMd5(string md5hash);

        Task<IEnumerable<FileModel>> GetAllByMd5(string md5hash);

        Task<bool> AnyBySha1(string sha1hash);
    }
}
