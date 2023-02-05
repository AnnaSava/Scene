using SavaDev.Base.Data.Managers.Crud;
using SavaDev.Base.Data.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace SavaDev.Base.User.Data.Models.Interfaces
{
    public abstract  class BaseRoleModel : IModel<long>, IFormModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }

        public IEnumerable<string> Permissions { get; set; }
    }
}
