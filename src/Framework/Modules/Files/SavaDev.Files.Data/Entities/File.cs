using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Files.Data.Entities
{
    public class File
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Ext { get; set; }

        public string MimeType { get; set; }

        public byte[] Content { get; set; }

        public string Md5 { get; set; }

        public string Sha1 { get; set; }

        public string Owner { get; set; }

        public long Size { get; set; }

        public DateTime DateUploaded { get; set; } = DateTime.UtcNow;

        public bool IsDuplicateMd5 { get; set; }

        public bool IsDuplicateSha1 { get; set; }

        public bool IsDeleted { get; set; }
    }
}
