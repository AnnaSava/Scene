using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Base.Unit;
using SavaDev.Base.Unit.Options;
using SavaDev.Files.Data;
using SavaDev.Files.Data.Contract;
using SavaDev.Files.Data.Services;
using SavaDev.Files.Front.Contract;
using SavaDev.Files.Front.Services;

namespace Sava.Files
{
    public static class FilesViewUnit
    {
        public static void AddFiles(this IServiceCollection services, IConfiguration config, UnitOptions unitOptions)
        {
            services.AddUnitDbContext<FilesContext>(config, unitOptions);

            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IFileViewService, FileViewService>();
        }
    }
}