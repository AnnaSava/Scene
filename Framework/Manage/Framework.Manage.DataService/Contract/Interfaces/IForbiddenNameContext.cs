using Framework.Manage.DataService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Manage.DataService.Contract.Interfaces
{
    public interface IForbiddenNameContext
    {
        DbSet<ForbiddenName> ForbiddenNames { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
