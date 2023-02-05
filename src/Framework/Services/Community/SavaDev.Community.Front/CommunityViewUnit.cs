using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Base.Front.Options;
using SavaDev.Community.Data;
using SavaDev.Community.Data.Contract;
using SavaDev.Community.Data.Services;
using SavaDev.Community.Service.Contract;

namespace SavaDev.Community.Service
{
    public static class CommunityViewUnit
    {
        public static void AddCommunity(this IServiceCollection services, IConfiguration config, ServiceOptions serviceOptions)
        {
            /// todo
            //services.AddUnitDbContext<CommunityContext>(config, moduleSettings);

            services.AddScoped<ICommunityService, CommunityService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<ILockoutService, LockoutService>();
        }

    }
}