﻿using SavaDev.Base.Data.Services;
using SavaDev.Scheme.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Data.Contract
{
    public interface ITableService
    {
        Task<OperationResult> Create(TableModel model);
    }
}
