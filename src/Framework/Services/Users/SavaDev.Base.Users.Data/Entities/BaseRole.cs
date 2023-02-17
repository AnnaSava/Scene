using Microsoft.AspNetCore.Identity;
using SavaDev.Base.Data.Entities.Interfaces;
using System;

namespace SavaDev.Base.User.Data.Entities
{
    public class BaseRole : IdentityRole<long>, IEntityRestorable, IEntity<long>, ICloneable
    {
        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
