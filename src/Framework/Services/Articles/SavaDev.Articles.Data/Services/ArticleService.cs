using AutoMapper;
using Sava.Articles.Data;
using Sava.Articles.Data.Entities;
using SavaDev.Base.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Articles.Data.Services
{
    public class ArticleService : BaseEntityService<Article, ArticleModel>, IArticleService
    {
        public ArticleService(ArticlesContext dbContext, IMapper mapper) : base(dbContext, mapper, nameof(ArticleService))
        {

        }
    }
}
