using Framework.Base.DataService.Contract.Models;
using System;

namespace Sava.Forums.Data
{
    public class TopicModel : BaseAliasedModel<long>
    {
        public long ForumId { get; set; }

        public ForumModel Forum { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public bool IsClosed { get; set; }

        public DateTime Date { get; set; }

        public DateTime LastAnswered { get; set; }

        public int Posts { get; set; }

        public int Views { get; set; }

    }
}
