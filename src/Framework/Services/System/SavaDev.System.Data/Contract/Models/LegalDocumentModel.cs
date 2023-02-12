using SavaDev.Base.Data.Enums;
using SavaDev.Base.Data.Models;
using SavaDev.Base.Data.Models.Interfaces;

namespace SavaDev.System.Data.Contract.Models
{
    public class LegalDocumentModel : BaseDocumentFormModel<long>, IModel<long>
    {
        public string Info { get; set; }

        public string Comment { get; set; }
    }
}
