﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SavaDev.Content.Contract;
using SavaDev.Content.Data;
using SavaDev.Content.Data.Contract;
using SavaDev.Content.Data.Services;
using SavaDev.Content.Services;
using SavaDev.Base.Unit.Options;
using SavaDev.Base.Unit;

namespace SavaDev.Content
{
    public static class ContentFrontUnit
    {
        public static void AddContent(this IServiceCollection services, IConfiguration config, UnitOptions unitOptions)
        {
            services.AddUnitDbContext<ContentContext>(config, unitOptions);

            services.AddScoped<IDraftService, DraftService>();
            services.AddScoped<IVersionService, VersionService>();
            services.AddScoped<IExportService, ExportService>();
            services.AddScoped<IImportService, ImportService>();
        }

        public static void AddContentFront(this IServiceCollection services)
        {
            services.AddScoped<IDraftFrontService, DraftFrontService>();
            services.AddScoped<IVersionFrontService, VersionFrontService>();
        }
    }
}