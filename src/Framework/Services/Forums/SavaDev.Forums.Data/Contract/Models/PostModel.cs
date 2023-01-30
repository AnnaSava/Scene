using Framework.Base.DataService.Contract.Models;
using System;

namespace Sava.Forums.Data
{
    public class PostModel : BaseRestorableModel<long>
    {
        public long TopicId { get; set; }

        public string Author { get; set; }

        public string IpAddress { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }
    }
}
