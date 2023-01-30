using Framework.Base.Service.Contract.Models;

namespace Sava.Forums
{
    public class ForumViewModel : BaseViewModel<long>
    {
        public string Module { get; set; }

        public string Alias { get; set; }

        public string Title { get; set; }

        public string Info { get; set; }

        public int Topics { get; set; }

        public int Posts { get; set; }
    }
}
