using AutoMapper;
using Framework.Base.DataService.Services;
using Sava.Articles.Data;
using Sava.Articles.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Articles.Data.Services
{
    public class ArticleService : AliasedEntityService<Article, ArticleModel>, IArticleService
    {
        public ArticleService(ArticlesContext dbContext, IMapper mapper) : base(dbContext, mapper, nameof(ArticleService))
        {

        }
    }
}
