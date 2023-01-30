using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SavaDev.Boxyz.Service
{
    public static class BoxyzModule
    {
        public static void AddBoxyz(this IServiceCollection services, IConfiguration config, ModuleSettings moduleSettings)
        {
            // TODO
            //services.AddModuleDbContext<BoxyzContext>(config, moduleSettings);

        }
    }
}