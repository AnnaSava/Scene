using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Base.Unit;
using SavaDev.Base.Unit.Options;
using SavaDev.Content.Data;
using SavaDev.Content.Data.Contract;
using SavaDev.Content.Data.Services;
using SavaDev.Content.View.Contract;
using SavaDev.Content.View.Services;

namespace SavaDev.Content.View
{
    public static class ContentViewUnit
    {
        public static void AddContent(this IServiceCollection services, IConfiguration config, UnitOptions unitOptions)
        {
            services.AddUnitDbContext<ContentContext>(config, unitOptions);

            services.AddScoped<IDraftService, DraftService>();
            services.AddScoped<IVersionService, VersionService>();
            services.AddScoped<IExportService, ExportService>();
            services.AddScoped<IImportService, ImportService>();
        }

        public static void AddContentView(this IServiceCollection services)
        {
            services.AddScoped<IDraftViewService, DraftViewService>();
            services.AddScoped<IVersionViewService, VersionViewService>();
        }
    }
}