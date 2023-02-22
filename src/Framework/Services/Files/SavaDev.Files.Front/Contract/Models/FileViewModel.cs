using SavaDev.Base.Front.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Files.Front.Contract.Models
{
    public class FileViewModel : BaseRegistryItemViewModel<Guid>
    {
        public string Name { get; set; }

        public string Ext { get; set; }

        public string MimeType { get; set; }

        public byte[] Content { get; set; }

        public string Md5 { get; set; }

        public string Sha1 { get; set; }

        public string Owner { get; set; }

        public long Size { get; set; }

        public DateTime DateUploaded { get; set; }

        public bool IsDuplicateMd5 { get; set; }

        public bool IsDuplicateSha1 { get; set; }

        public bool IsDeleted { get; set; }
    }
}
