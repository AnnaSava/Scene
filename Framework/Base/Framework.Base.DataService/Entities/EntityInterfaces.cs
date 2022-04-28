using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Entities
{
    public interface IAnyEntity { }

    public interface IEntity<TKey> : IAnyEntity
    {
        public TKey Id { get; set; }
    }

    public interface IEntityUpdatable : IAnyEntity
    {
        public DateTime LastUpdated { get; set; }
    }

    public interface IEntityRestorable : IEntityUpdatable
    {
        public bool IsDeleted { get; set; }
    }

    public interface IEntityAliased : IAnyEntity
    {
        public string Alias { get; set; }
    }
}
