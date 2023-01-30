using Microsoft.AspNetCore.Identity;
using SavaDev.Base.Data.Entities.Interfaces;
using System;

namespace SavaDev.Base.User.Data.Entities
{
    public class BaseRole : IdentityRole<long>, IEntityRestorable, IEntity<long>
    {
        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }
    }
}
