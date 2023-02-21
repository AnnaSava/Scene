using Sava.Media.Contract.Models;
using Sava.Media.Data.Contract.Models;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Media.Contract
{
    public interface IImageViewService
    {
        Task<ImageViewModel> SaveImage(Stream stream, Guid? galleryId);

        Task<ImageViewModel> DownloadAndSaveImage(string fileUri, Guid? galleryId);

        Task<RegistryPageViewModel<ImageViewModel>> GetRegistryPage(RegistryQuery query);
    }
}
