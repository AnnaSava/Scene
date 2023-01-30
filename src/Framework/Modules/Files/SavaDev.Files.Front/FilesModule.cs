using AutoMapper;
using Framework.Base.Service.Module;
using Framework.Helpers.Files;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sava.Files.Contract;
using Sava.Files.Data;
using Sava.Files.Data.Contract;
using Sava.Files.Data.Services;

namespace Sava.Files
{
    public static class FilesModule
    {
        public static void AddFiles(this IServiceCollection services, IConfiguration config, ModuleSettings moduleSettings)
        {
            services.AddModuleDbContext<FilesContext>(config, moduleSettings);

            services.AddScoped<IFileService>(
                s => new FileService(
                    s.GetService<FilesContext>(),
                    s.GetService<IMapper>()));

            services.AddScoped<MimeTypeChecker>();
            services.AddScoped<HashHelper>();

            services.AddScoped<IFileProcessingService, FileProcessingService>();
        }
    }
}