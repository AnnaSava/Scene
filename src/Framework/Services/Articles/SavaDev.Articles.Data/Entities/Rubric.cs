using SavaDev.Base.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Articles.Data.Entities
{
    public class Rubric : BaseAliasedEntity<long>
    {
        public string Section { get; set; }

        public string Title { get; set; }

        public int ArticlesCount { get; set; }

        public virtual ICollection<ArticleRubric> Articles { get; set; }
    }
}
