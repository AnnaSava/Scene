using SavaDev.Base.Data.Enums;
using SavaDev.Base.Data.Models;
using SavaDev.Base.Data.Models.Interfaces;

namespace SavaDev.System.Data.Contract.Models
{
    public class LegalDocumentModel : BaseDocumentFormModel<long>, IModel<long>
    {
        public string PermName { get; set; }

        public string Culture { get; set; }

        public string Title { get; set; }

        public string Info { get; set; }

        public string Text { get; set; }

        public string Comment { get; set; }

        public DateTime Created { get; set; }

        public DocumentStatus Status { get; set; }
    }
}
