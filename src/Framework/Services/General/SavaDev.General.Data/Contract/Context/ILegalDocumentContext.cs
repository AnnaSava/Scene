using Microsoft.EntityFrameworkCore;
using SavaDev.General.Data.Entities;

namespace SavaDev.General.Data.Contract.Context
{
    public interface ILegalDocumentContext
    {
        DbSet<LegalDocument> LegalDocuments { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
