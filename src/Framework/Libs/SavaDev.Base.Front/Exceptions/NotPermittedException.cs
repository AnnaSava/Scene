﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Front.Exceptions
{
    public class NotPermittedException : Exception
    {
        public NotPermittedException()
            : base("Action is not permitted")
        {
        }
    }
}
