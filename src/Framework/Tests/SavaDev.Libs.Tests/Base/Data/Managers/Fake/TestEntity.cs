using SavaDev.Base.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Libs.Tests.Base.Data.Managers.Fake
{
    internal class TestEntity : IEntity<long>, IEntityRestorable
    {
        public long Id { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }
    }
}
