using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Files.Service.Contract;
using SavaDev.Files.Service.Services;
using SavaDev.Infrastructure.Util.ImageEditor;
using System.Net.Mail;

namespace SavaDev.Files.Service
{
    public static class FilesServiceUnit
    {
        public static void AddFilesRmqService(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<MimeTypeChecker>();
            services.AddScoped<HashHelper>();
            services.AddScoped<IFileProcessingService, FileProcessingService>();
            services.AddScoped<IFilesUploader, FilesRmqUploader>();
        }

        public static void AddFilesDirectService(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<MimeTypeChecker>();
            services.AddScoped<HashHelper>();
            services.AddScoped<IFileProcessingService, FileProcessingService>();
            services.AddScoped<IFilesUploader, FilesDirectUploader>();
        }
    }
}