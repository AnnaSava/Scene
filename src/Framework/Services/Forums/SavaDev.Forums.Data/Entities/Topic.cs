using SavaDev.Base.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sava.Forums.Data.Entities
{
    public class Topic : BaseAliasedEntity<long>
    {
        public long ForumId { get; set; }

        public virtual Forum Forum { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public bool IsClosed { get; set; }

        public DateTime Date { get; set; }

        public DateTime LastAnswered { get; set; }

        public int Posts { get; set; }

        public int Views { get; set; }

        public virtual ICollection<Post> TopicPosts { get; set; }
    }
}
