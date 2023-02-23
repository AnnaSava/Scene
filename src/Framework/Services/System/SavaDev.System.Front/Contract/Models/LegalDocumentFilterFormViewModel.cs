﻿using SavaDev.Base.Data.Enums;
using SavaDev.Base.Data.Registry.Filter;
using SavaDev.Base.Front.Registry;
using SavaDev.Base.Front.Registry.Filter;

namespace SavaDev.System.Front.Contract.Models
{
    public class LegalDocumentFilterFormViewModel : BaseFilter
    {
        public WordField PermName { get; set; } = new WordField();

        public WordField Title { get; set; } = new WordField();

        public WordField Culture { get; set; } = new WordField();

        public DocumentStatus? Status { get; set; }
    }
}
