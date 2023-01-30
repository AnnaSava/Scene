using Sava.Media.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Media.Contract
{
    public interface IGalleryProcessor
    {
        void Configure(ImageProcessorOptions options);

        Task<ImageViewModel> SaveImage(Stream stream, ImageSavingModel model);

        Task<ImageViewModel> DownloadAndSaveImage(string fileUri, ImageSavingModel model);
    }
}
