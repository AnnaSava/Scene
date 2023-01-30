using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Types.ModelTypes
{
    [Obsolete]
    public interface IAnyModel { }

    [Obsolete]
    public interface IModel<TKey> : IAnyModel
    {
        public TKey Id { get; set; }
    }

    [Obsolete]
    public interface IModelUpdatable : IAnyModel
    {
        public DateTime LastUpdated { get; set; }
    }

    [Obsolete]
    public interface IModelRestorable : IModelUpdatable
    {
        public bool IsDeleted { get; set; }
    }
    [Obsolete]
    public interface IModelAliased : IAnyModel
    {
        public string Alias { get; set; }
    }
}
