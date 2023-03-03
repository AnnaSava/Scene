using SavaDev.Base.Data.Entities;

namespace SavaDev.General.Data.Entities
{
    public class LegalDocument : BaseDocumentEntity<long>
    {
        public string? Info { get; set; }

        public string? Comment { get; set; }
    }
}
