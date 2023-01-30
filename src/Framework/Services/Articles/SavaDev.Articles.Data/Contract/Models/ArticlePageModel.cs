using Framework.Base.DataService.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
