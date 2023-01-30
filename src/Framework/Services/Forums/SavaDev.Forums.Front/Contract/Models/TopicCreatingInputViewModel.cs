using System.ComponentModel.DataAnnotations;

namespace Sava.Forums
{
    public class TopicCreatingInputViewModel
    {
        public long Id { get; set; }

        public long ForumId { get; set; }

        [Required]
        public string Alias { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public string IpAddress { get; set; }

        [Required]
        public string Text { get; set; }

        public string Date { get; set; }
    }
}
