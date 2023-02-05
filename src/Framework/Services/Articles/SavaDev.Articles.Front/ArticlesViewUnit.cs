using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Base.Unit.Options;

namespace Sava.Articles
{
    public static class ArticlesViewUnit
    {
        public static void AddArticles (this IServiceCollection services, IConfiguration config, UnitOptions unitOptions)
        {
            //services.AddUnitDbContext<ArticlesContext>(config, unitOptions);

            //services.AddScoped<IArticleService, ArticleService>();
            //services.AddScoped<IRubricService, RubricService>();

            // TODO зочем это здесь?
            //services.AddScoped<IForumViewService, ForumViewService>();
        }
    }
}
