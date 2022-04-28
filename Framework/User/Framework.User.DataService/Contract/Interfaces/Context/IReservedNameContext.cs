using Framework.User.DataService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Interfaces
{
    public interface IReservedNameContext
    {
        DbSet<ReservedName> ReservedNames { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
