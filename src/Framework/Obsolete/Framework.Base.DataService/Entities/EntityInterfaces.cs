using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Entities
{
    [Obsolete]
    public interface IAnyEntity { }
    [Obsolete]
    public interface IEntity<TKey> : IAnyEntity
    {
        public TKey Id { get; set; }
    }
    [Obsolete]
    public interface IEntityUpdatable : IAnyEntity
    {
        public DateTime LastUpdated { get; set; }
    }
    [Obsolete]
    public interface IEntityRestorable : IEntityUpdatable
    {
        public bool IsDeleted { get; set; }
    }
}
