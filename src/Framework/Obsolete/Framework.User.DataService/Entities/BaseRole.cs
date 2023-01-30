using Framework.Base.DataService.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Framework.User.DataService.Entities
{
    // TODO Подумать о вынесении в отдельную сборку
    public class BaseRole : IdentityRole<long>, IEntityRestorable, IEntity<long>
    {
        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }
    }
}
