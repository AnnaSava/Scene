using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Articles.Data
{
   public class ArticleTagModel
    {
        public long ArticleId { get; set; }

        public ArticleModel Article { get; set; }

        public string NormalizedTag { get; set; }

        public string Tag { get; set; }
    }
}
