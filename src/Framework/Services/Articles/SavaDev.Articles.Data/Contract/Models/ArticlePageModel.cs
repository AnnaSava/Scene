namespace Sava.Articles.Data
{
    public class ArticlePageModel
    {
        public long ArticleId { get; set; }

        public ArticleModel Article { get; set; }

        public int PageNumber { get; set; }

        public string Text { get; set; }
    }
}
