using Framework.Base.Service.Module;
using Framework.Community.Data;
using Framework.Community.Data.Contract;
using Framework.Community.Data.Services;
using Framework.Community.Service.Contract;
using Framework.Community.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Community.Service
{
    public static class CommunityModule
    {
        public static void AddCommunity(this IServiceCollection services, IConfiguration config, ModuleSettings moduleSettings)
        {
            services.AddModuleDbContext<CommunityContext>(config, moduleSettings);

            services.AddScoped<ICommunityService, CommunityService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<ILockoutService, LockoutService>();
        }

    }
}