using System.ComponentModel.DataAnnotations;

namespace Sava.Articles.Data.Entities
{
    public class ArticlePage
    {
        [Key]
        public long ArticleId { get; set; }

        public virtual Article Article { get; set; }

        [Key]
        public int PageNumber { get; set; }

        public string Text { get; set; }
    }
}
