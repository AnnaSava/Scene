﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Contract.Models
{
    public class PermissionTreeNodeViewModel
    {
        public string Group { get; set; }

        public bool IsChecked { get; set; }

        public List<PermissionViewModel> Permissions { get; set; }
    }
}
