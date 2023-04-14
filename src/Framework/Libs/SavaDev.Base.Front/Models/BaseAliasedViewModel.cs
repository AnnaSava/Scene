using SavaDev.Base.Data.Models.Interfaces;
using SavaDev.Base.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Front.Models
{
    public class BaseAliasedViewModel<TKey> : BaseRestorableViewModel<TKey>, IModelAliased
    {
        public string Alias { get; set; }
    }
}
