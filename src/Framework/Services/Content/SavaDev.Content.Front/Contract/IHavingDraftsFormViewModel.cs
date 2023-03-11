using SavaDev.Content.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Content.Front.Contract
{
    public interface IHavingDraftsFormViewModel
    {
        public IEnumerable<DraftViewModel> Drafts { get; set; }

        public bool HasMoreDrafts { get; set; }
    }
}
