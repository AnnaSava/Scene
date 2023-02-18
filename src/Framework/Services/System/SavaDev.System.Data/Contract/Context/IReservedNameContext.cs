using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.System.Data.Entities;

namespace SavaDev.System.Data.Contract.Context
{
    public interface IReservedNameContext : IDbContext
    {
        DbSet<ReservedName> ReservedNames { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
