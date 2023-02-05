using SavaDev.Base.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Sava.Articles.Data
{
    public class ArticleModel : BaseAliasedModel<long>
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public string Author { get; set; }

        public string PreviewImage { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? PublishDate { get; set; }

        public bool IsDraft { get; set; }

        public long ViewsCount { get; set; }

        public string Section { get; set; }

        public ICollection<ArticlePageModel> Pages { get; set; }

        public ICollection<ArticleDateModel> Dates { get; set; }

        public ICollection<ArticleTagModel> Tags { get; set; }

        public ICollection<RubricModel> Rubrics { get; set; }
    }
}
