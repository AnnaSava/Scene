using Microsoft.EntityFrameworkCore;
using SavaDev.General.Data.Entities;
using SavaDev.General.Data.Entities.Parts;

namespace SavaDev.General.Data.Contract.Context
{
    public interface IPermissionContext
    {
        DbSet<Permission> Permissions { get; set; }

        DbSet<PermissionCulture> PermissionCultures { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
