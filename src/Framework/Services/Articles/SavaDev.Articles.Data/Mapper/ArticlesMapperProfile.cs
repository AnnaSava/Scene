using AutoMapper;
using Sava.Articles.Data;
using Sava.Articles.Data.Entities;

namespace Sava.Articles.Data.Mapper
{
    public class ArticlesMapperProfile : Profile
    {
        public ArticlesMapperProfile()
        {
            CreateMap<Article, ArticleModel>();
            CreateMap<ArticleModel, Article>();

            CreateMap<Rubric, RubricModel>();
            CreateMap<RubricModel, Rubric>();
        }
    }
}
