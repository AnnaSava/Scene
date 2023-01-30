using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Base.Service.Contract.Models
{
   public class BaseViewModel<TKey>
    {
        public TKey Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
