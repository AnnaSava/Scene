using System.ComponentModel.DataAnnotations;

namespace Sava.Forums
{
    public class PostInputViewModel
    {
        public long Id { get; set; }

        public long TopicId { get; set; }

        [Required]
        public string Author { get; set; }

        public string IpAddress { get; set; }

        [Required]
        public string Text { get; set; }

        public string Date { get; set; }

        public string TopicName { get; set; }
    }
}
