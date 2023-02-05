using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Savadev.Content.Contract;
using Savadev.Content.Data;
using Savadev.Content.Data.Contract;
using Savadev.Content.Data.Services;
using Savadev.Content.Services;
using SavaDev.Base.Front.Options;
using SavaDev.Base.Unit;
using SavaDev.Content.Data;
using SavaDev.Content.Data.Services;

namespace Savadev.Content
{
    public static class ContentViewUnit
    {
        public static void AddContent(this IServiceCollection services, IConfiguration config, ServiceOptions serviceOptions)
        {
            services.AddUnitDbContext<ContentContext>(config, serviceOptions);

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