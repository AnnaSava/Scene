using SavaDev.Content.View.Contract.Models;

namespace SavaDev.Content.View.Contract
{
    public interface IHavingDraftsFormViewModel
    {
        public IEnumerable<DraftViewModel> Drafts { get; set; }

        public bool HasMoreDrafts { get; set; }
    }
}
