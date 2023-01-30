using Microsoft.EntityFrameworkCore;
using SavaDev.System.Data.Entities;

namespace SavaDev.System.Data.Contract.Context
{
    public interface ILegalDocumentContext
    {
        DbSet<LegalDocument> LegalDocuments { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
