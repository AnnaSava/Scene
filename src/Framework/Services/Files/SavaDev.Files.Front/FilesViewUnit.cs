﻿using Framework.Helpers.Files;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sava.Files.Contract;
using SavaDev.Base.Unit.Options;
using SavaDev.Base.Unit;
using SavaDev.Files.Data;
using SavaDev.Files.Data.Contract;
using SavaDev.Files.Data.Services;

namespace Sava.Files
{
    public static class FilesViewUnit
    {
        public static void AddFiles(this IServiceCollection services, IConfiguration config, UnitOptions unitOptions)
        {
            services.AddUnitDbContext<FilesContext>(config, unitOptions);

            services.AddScoped<IFileService, FileService>();

            services.AddScoped<MimeTypeChecker>();
            services.AddScoped<HashHelper>();

            services.AddScoped<IFileProcessingService, FileProcessingService>();
        }
    }
}