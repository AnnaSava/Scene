using SavaDev.Base.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Articles.Data
{
    public class RubricModel : BaseAliasedModel<long>
    {
        public string Title { get; set; }

        public ICollection<ArticleModel> Articles { get; set; }
    }
}
