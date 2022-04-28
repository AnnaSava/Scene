using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Types.ModelTypes
{
    public interface IAnyModel { }

    public interface IModel<TKey> : IAnyModel
    {
        public TKey Id { get; set; }
    }

    public interface IModelUpdatable : IAnyModel
    {
        public DateTime LastUpdated { get; set; }
    }

    public interface IModelRestorable : IModelUpdatable
    {
        public bool IsDeleted { get; set; }
    }

    public interface IModelAliased : IAnyModel
    {
        public string Alias { get; set; }
    }
}
