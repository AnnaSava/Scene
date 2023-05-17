using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Files.Service.Contract.Models
{
    public class FilesDataModel
    {
        public byte[] Content { get; set; }

        public string OwnerId { get; set; }

        public FilesDataModel(byte[] content, string ownerId)
        {
            Content = content;
            OwnerId = ownerId;
        }
    }
}
