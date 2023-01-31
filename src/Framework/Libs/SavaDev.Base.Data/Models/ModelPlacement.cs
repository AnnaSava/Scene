using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Models
{
    public class ModelPlacement
    {
        public string Entity { get; }

        public string Module { get; }

        public ModelPlacement(Type type)
        {
            var module = type.Assembly.GetName().Name;
            var entity = type.Name;

            Entity = entity;
            Module = module;
        }
    }
}
