using Framework.Base.Service.Module;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sava.Articles
{
    public static class ArticlesModule
    {
        public static void AddArticles(this IServiceCollection services, IConfiguration config, ModuleSettings moduleSettings)
        {
            //services.AddModuleDbContext<ArticlesContext>(config, moduleSettings);

            //services.AddScoped<IArticleService>(
            //    s => new ArticleService(
            //        s.GetService<ArticlesContext>(),
            //        s.GetService<IMapper>()));

            //services.AddScoped<IRubricService>(
            //    s => new RubricService(
            //        s.GetService<ArticlesContext>(),
            //        s.GetService<IMapper>()));

            //services.AddScoped<IForumViewService>(
            //    s => new ForumViewService(
            //        s.GetService<IForumService>(),
            //        s.GetService<ITopicService>(),
            //        s.GetService<IPostService>(),
            //        s.GetService<IMapper>()));
        }
    }
}
