﻿using Framework.Base.Types.Enums;
using SavaDev.Base.Front.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.System.Front.Contract.Models
{
    public class MailTemplateViewModel : BaseRegistryItemViewModel
    {
        public long Id { get; set; }

        public string PermName { get; set; }

        public string Culture { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime Created { get; set; }

        public DocumentStatus Status { get; set; }

        public bool HasAllTranslations { get; set; }

        public bool IsDeleted { get; set; }
    }
}
