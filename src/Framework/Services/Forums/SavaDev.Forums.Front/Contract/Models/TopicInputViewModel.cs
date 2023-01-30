using System.ComponentModel.DataAnnotations;

namespace Sava.Forums
{
    public class TopicInputViewModel
    {
        public long Id { get; set; }

        public long ForumId { get; set; }

        [Required]
        public string Alias { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public int Views { get; set; }
    }
}
