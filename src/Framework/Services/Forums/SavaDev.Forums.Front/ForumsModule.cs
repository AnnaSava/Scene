using AutoMapper;
using Framework.Base.Service.Module;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sava.Forums;
using Sava.Forums.Data;
using Sava.Forums.Data.Services;
using Sava.Forums.Services;

namespace Sava.Forums
{
    public static class ForumsModule
    {
        public static void AddForum(this IServiceCollection services, IConfiguration config, ModuleSettings moduleSettings)
        {
            services.AddModuleDbContext<ForumsContext>(config, moduleSettings);

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

            services.AddScoped<IForumViewService>(
                s => new ForumViewService(
                    s.GetService<IForumService>(),
                    s.GetService<ITopicService>(),
                    s.GetService<IPostService>(),
                    s.GetService<IMapper>()));
        }
    }
}
