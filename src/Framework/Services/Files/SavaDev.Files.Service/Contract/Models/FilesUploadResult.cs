using SavaDev.Files.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Files.Service.Contract.Models
{
    public class FilesUploadResult
    {
        public FileModel SavedFile { get; set; } // TODO точно ссылку на дату?
    }
}
