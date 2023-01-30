using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Models.Interfaces
{
    public interface IModel<TKey> : IAnyModel
    {
        public TKey Id { get; set; }
    }
}
