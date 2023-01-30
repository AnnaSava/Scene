using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Articles.Data.Entities
{
    public class ArticleRubric
    {
        public long ArticleId { get; set; }

        public Article Article { get; set; }

        public long RubricId { get; set; }

        public Rubric Rubric { get; set; }
    }
}
