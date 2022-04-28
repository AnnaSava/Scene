using Framework.User.DataService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Interfaces
{
    public interface IConsentContext
    {
        DbSet<Consent> Consents { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
