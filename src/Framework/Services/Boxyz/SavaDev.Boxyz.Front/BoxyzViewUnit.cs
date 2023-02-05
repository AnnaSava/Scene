using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Base.Unit;
using SavaDev.Base.Unit.Options;
using SavaDev.Boxyz.Data;

namespace SavaDev.Boxyz.Service
{
    public static class BoxyzViewUnit
    {
        public static void AddBoxyz(this IServiceCollection services, IConfiguration config, UnitOptions unitOptions)
        {
            services.AddUnitDbContext<BoxyzContext>(config, unitOptions);
        }
    }
}