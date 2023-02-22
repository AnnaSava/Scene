using AutoMapper;
using SavaDev.Files.Data;
using SavaDev.Files.Front;

namespace Scene.Files.WebApi
{
    public static class MapperConfig
    {
        public static void AddMapper(this IServiceCollection services)
        {
            // Auto Mapper Configurations
            // Можно добавлять все профили из сборки, но в нашем случае всё равно прописывать каждую сборку - надо разбираться
            // https://stackoverflow.com/questions/2651613/how-to-scan-and-auto-configure-profiles-in-automapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new FilesMapperProfile());
                mc.AddProfile(new FilesViewMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
