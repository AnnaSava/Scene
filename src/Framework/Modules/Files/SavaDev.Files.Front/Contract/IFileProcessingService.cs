using Sava.Files.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Files.Contract
{
    public interface IFileProcessingService
    {
        Task<FileModel> UploadFilePreventDuplicate(byte[] content);
    }
}
