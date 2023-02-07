using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Base.Unit;
using SavaDev.Base.Unit.Options;
using SavaDev.Community.Data;
using SavaDev.Community.Data.Contract;
using SavaDev.Community.Data.Services;
using SavaDev.Community.Service.Contract;

namespace SavaDev.Community.Service
{
    public static class CommunityViewUnit
    {
        public static void AddCommunity (this IServiceCollection services, IConfiguration config, UnitOptions unitOptions)
        {
            services.AddUnitDbContext<CommunityContext>(config, unitOptions);

            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<ILockoutService, LockoutService>();
        }

    }
}