using System.ComponentModel.DataAnnotations;

namespace Sava.Forums
{
    public class ForumInputViewModel 
    {
        public long Id { get; set; } 

        [Required]
        public string Module { get; set; }

        [Required]
        public string Alias { get; set; }

        [Required]
        public string Title { get; set; }

        public string Info { get; set; }

        public int Topics { get; set; }

        public int Posts { get; set; }
    }
}
