using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Types
{
    public class ModelPlacement
    {
        public string Entity { get; }

        public string Module { get; }

        public ModelPlacement(Type type)
        {
            var t = type.Assembly.GetName().Name;
            var t1 = type.Name;

            Entity= t1;
            Module = t;
        }
    }
}
