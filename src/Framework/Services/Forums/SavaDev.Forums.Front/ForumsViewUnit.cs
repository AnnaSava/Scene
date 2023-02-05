using AutoMapper;
using Framework.Base.Service.Module;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sava.Forums;
using Sava.Forums.Data;
using Sava.Forums.Data.Services;
using Sava.Forums.Services;
using SavaDev.Base.Unit.Options;
using SavaDev.Base.Unit;

namespace Sava.Forums
{
    public static class ForumsViewUnit
    {
        public static void AddForums (this IServiceCollection services, IConfiguration config, UnitOptions unitOptions)
        {
            services.AddUnitDbContext<ForumsContext>(config, unitOptions);

            services.AddScoped<IForumService>(
                s => new ForumService(
                    s.GetService<ForumsContext>(),
                    s.GetService<IMapper>()));

            services.AddScoped<ITopicService>(
                s => new TopicService(
                    s.GetService<ForumsContext>(),
                    s.GetService<IMapper>()));

            services.AddScoped<IPostService>(
                s => new PostService(
                    s.GetService<ForumsContext>(),
                    s.GetService<IMapper>()));

            //services.AddScoped<IForumViewService, ForumViewService>();
        }
    }
}
