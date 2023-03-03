using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Content.Contract.Models
{
    public class VersionViewModel
    {
        public Guid Id { get; set; }

        public string Entity { get; set; }

        public string Module { get; set; }

        public string Owner { get; set; }

        public string ContentId { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }
    }
}
