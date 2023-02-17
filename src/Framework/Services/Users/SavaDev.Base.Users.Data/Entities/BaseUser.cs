using Microsoft.AspNetCore.Identity;
using SavaDev.Base.Data.Entities.Interfaces;
using System;

namespace SavaDev.Base.User.Data.Entities
{
    public abstract class BaseUser : IdentityUser<long>, IEntityRestorable, IEntity<long>, ICloneable
    {
        public DateTime RegDate { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
