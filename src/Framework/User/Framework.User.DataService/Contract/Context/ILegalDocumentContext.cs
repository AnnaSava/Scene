using Framework.User.DataService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Interfaces.Context
{
    [Obsolete]
    public interface ILegalDocumentContext
    {
        DbSet<LegalDocument> LegalDocuments { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
