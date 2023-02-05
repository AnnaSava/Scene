using Framework.Base.DataService.Entities;
using Microsoft.AspNetCore.Identity;
using System;

namespace Framework.User.DataService.Entities
{
    [Obsolete]
    // TODO Подумать о вынесении в отдельную сборку
    public abstract class BaseUser : IdentityUser<long>, IEntityRestorable, IEntity<long>
    {
        public DateTime RegDate { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }
    }
}
