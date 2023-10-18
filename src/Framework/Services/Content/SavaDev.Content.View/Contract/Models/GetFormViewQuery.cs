using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SavaDev.Content.View.Contract.Models
{
    public class GetFormViewQuery
    {
        public long? Id { get; set; }

        public Guid? DraftId { get; set; }

        public string EntityCode { get; set; }

        public string ModuleCode { get; set; }

        public string UserId { get; set; }

        public bool CanCreate { get; set; }

        public bool CanUpdate { get; set; }

        public bool CanCreateFromDraft { get; set; }

        public bool CanGetAnyDrafts { get; set; }

        public bool CanGetSelfDrafts { get; set; }
    }
}
