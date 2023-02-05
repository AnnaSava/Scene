using SavaDev.Base.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Articles.Data.Entities
{
    public class Article : BaseAliasedEntity<long>
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

        public virtual ICollection<ArticlePage> Pages { get; set; }

        public virtual ICollection<ArticleDate> Dates { get; set; }

        public virtual ICollection<ArticleTag> Tags { get; set; }

        public virtual ICollection<ArticleRubric> Rubrics { get; set; }
    }
}
