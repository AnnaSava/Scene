using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.General.Data.Entities;

namespace SavaDev.General.Data.Contract.Context
{
    public interface IReservedNameContext : IDbContext
    {
        DbSet<ReservedName> ReservedNames { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
