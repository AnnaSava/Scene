using Microsoft.EntityFrameworkCore;
using SavaDev.System.Data.Entities;

namespace SavaDev.System.Data.Contract.Context
{
    public interface IReservedNameContext
    {
        DbSet<ReservedName> ReservedNames { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
