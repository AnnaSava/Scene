using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Community.Data;
using SavaDev.Community.Data.Contract;
using SavaDev.Community.Data.Services;
using SavaDev.Community.Service.Contract;

namespace SavaDev.Community.Service
{
    public static class CommunityModule
    {
        public static void AddCommunity(this IServiceCollection services, IConfiguration config, object t)//, ServiceOptions moduleSettings)
        {
            /// todo
            //services.AddModuleDbContext<CommunityContext>(config, moduleSettings);

            services.AddScoped<ICommunityService, CommunityService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<ILockoutService, LockoutService>();
        }

    }
}