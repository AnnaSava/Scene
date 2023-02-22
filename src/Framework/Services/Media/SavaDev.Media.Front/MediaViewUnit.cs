using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sava.Files.Data.Services;
using Sava.Media.Contract;
using Sava.Media.Data;
using Sava.Media.Data.Contract;
using Sava.Media.Services;
using SavaDev.Base.Unit;
using SavaDev.Base.Unit.Options;
using SavaDev.Infrastructure.Util.ImageEditor;
using SavaDev.Media.Front.Contract;
using SavaDev.Media.Front.Services;

namespace Sava.Media
{
    public static class MediaViewUnit
    {
        public static void AddMedia (this IServiceCollection services, IConfiguration config, UnitOptions unitOptions)
        {
            services.AddUnitDbContext<MediaContext>(config, unitOptions);

            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IGalleryService, GalleryService>();

            services.AddScoped<ImageEditor>();

            // TODO подумать над тем, чтобы сделать фабрику по аналогии с HttpClientFactory, т.к. в разных сервисах, вызывающих ImageProcessor, требуется разная конфигурация
            // и гипотетически возможно такое, что какой-то из сервисов перезапишет настройки до того, как предыдущий успеет сохранить изображения со своими настройками
            // AddTransient здесь не прокатит, потому что вроде Transient не инжектитсч в Scoped (но это не точно)
            // Пока нет сложных сценариев сохранения картинок разными сервисами в рамках одного запроса, можно оставить так
            services.AddScoped<IGalleryProcessor, GalleryProcessor>();

            services.AddScoped<IImageViewService, ImageViewService>();
            services.AddScoped<IGalleryViewService, GalleryViewService>();
        }
    }
}