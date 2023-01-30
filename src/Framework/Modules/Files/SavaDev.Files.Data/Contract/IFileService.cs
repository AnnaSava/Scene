using Framework.Base.DataService.Contract.Models.ListView;
using Sava.Files.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Files.Data.Contract
{
    public interface IFileService
    {
        Task<FileModel> Create(FileModel model);

        Task<FileModel> GetOne(Guid id);

        Task<PageListModel<FileModel>> GetAll(int page, int count);

        Task<bool> AnyByMd5(string md5hash);

        Task<IEnumerable<FileModel>> GetAllByMd5(string md5hash);

        Task<bool> AnyBySha1(string sha1hash);
    }
}
