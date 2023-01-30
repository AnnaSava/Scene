using System;

namespace Sava.Forums
{
    public class PostViewModel
    {
        public long Id { get; set; }

        public long TopicId { get; set; }

        public string Author { get; set; }

        public string IpAddress { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }
    }
}
