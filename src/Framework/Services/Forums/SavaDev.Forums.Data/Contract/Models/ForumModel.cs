using SavaDev.Base.Data.Models;
using SavaDev.Base.Data.Models.Interfaces;

namespace Sava.Forums.Data
{
    public class ForumModel : BaseAliasedModel<long>, IModelRestorable
    {
        public string Module { get; set; }

        public string Title { get; set; }

        public string Info { get; set; }

        public int Topics { get; set; }

        public int Posts { get; set; }
    }
}
