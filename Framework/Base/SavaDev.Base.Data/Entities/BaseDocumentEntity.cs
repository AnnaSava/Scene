using SavaDev.Base.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Entities
{
    public abstract class BaseDocumentEntity<TKey> : BaseRestorableEntity<TKey>
    {
        public string PermName { get; set; }

        public string Culture { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime Created { get; set; }

        public DocumentStatus Status { get; set; }
    }
}
