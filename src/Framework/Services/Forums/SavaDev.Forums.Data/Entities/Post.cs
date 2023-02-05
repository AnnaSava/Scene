using SavaDev.Base.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sava.Forums.Data.Entities
{
    public class Post : BaseRestorableEntity<long>
    {
        public long TopicId { get; set; }

        public virtual Topic Topic { get; set; }

        public string Author { get; set; }

        public string IpAddress { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }
    }
}
