using SavaDev.Files.Service.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Files.Service.Contract
{
    public interface IFilesUploader
    {
        Task<FilesUploadResult> SendInfo(FilesDataModel info);
    }
}
