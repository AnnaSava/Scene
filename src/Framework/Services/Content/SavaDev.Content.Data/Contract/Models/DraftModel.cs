using Savadev.Content.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savadev.Content.Data
{
    public class DraftModel<T> : BaseContentModel<T>
    {
        public string? ContentId { get; set; }

        public string? GroupingKey { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class DraftModel : DraftModel<string>
    {
    }
}
