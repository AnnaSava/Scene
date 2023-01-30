using System;

namespace Sava.Forums
{
    public class TopicViewModel
    {
        public long Id { get; set; }

        public string Alias { get; set; }

        public long ForumId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public bool IsClosed { get; set; }

        public DateTime Date { get; set; }

        public DateTime LastAnswered { get; set; }

        public int Posts { get; set; }

        public int Views { get; set; }
    }
}
