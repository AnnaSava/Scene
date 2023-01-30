using Framework.Base.DataService.Contract.Models;

namespace Sava.Forums.Data
{
    public class ForumModel : BaseAliasedModel<long>
    {
        public string Module { get; set; }

        public string Title { get; set; }

        public string Info { get; set; }

        public int Topics { get; set; }

        public int Posts { get; set; }
    }
}
