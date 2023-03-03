﻿using SavaDev.Content.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Content.Data
{
    public class ExportModel<T> : BaseContentModel<T>
    {
        public string ContentId { get; set; }
    }

    public class ExportModel : ExportModel<string>
    {

    }
}
