using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
using SavaDev.Files.Front.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Files.Front.Contract
{
    public interface IFileViewService
    {
        Task Upload(Stream stream);

        Task DownloadAndSaveFile(string fileUri);

        Task<RegistryPageViewModel<FileViewModel>> GetRegistryPage(RegistryQuery query);
    }
}
