using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Base.Front.Options;

namespace SavaDev.Boxyz.Service
{
    public static class BoxyzViewUnit
    {
        public static void AddBoxyz(this IServiceCollection services, IConfiguration config, ServiceOptions serviceOptions)
        {
            // TODO
            //services.AddUnitDbContext<BoxyzContext>(config, moduleSettings);

        }
    }
}