using Framework.Base.Service.Module;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Boxyz.Data;
using Microsoft.Extensions.Configuration;

namespace SavaDev.Boxyz.Service
{
    public static class BoxyzModule
    {
        public static void AddBoxyz(this IServiceCollection services, IConfiguration config, ModuleSettings moduleSettings)
        {
            services.AddModuleDbContext<BoxyzContext>(config, moduleSettings);

        }
    }
}