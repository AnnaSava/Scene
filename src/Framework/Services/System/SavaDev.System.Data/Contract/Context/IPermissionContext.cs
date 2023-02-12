﻿using Microsoft.EntityFrameworkCore;
using SavaDev.System.Data.Entities;
using SavaDev.System.Data.Entities.Parts;

namespace SavaDev.System.Data.Contract.Context
{
    public interface IPermissionContext
    {
        DbSet<Permission> Permissions { get; set; }

        DbSet<PermissionCulture> PermissionCultures { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
