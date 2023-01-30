using System;
using System.Collections.Generic;
using System.Text;

namespace SavaDev.Base.Front.Models
{
    public class BaseViewModel<TKey>
    {
        public TKey Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
