using SavaDev.Base.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Models
{
    public class BaseRestorableFormModel<TKey> : IModel<TKey>
    {
        public TKey Id { get; set; }
    }
}
