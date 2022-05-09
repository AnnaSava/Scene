using AutoMapper;
using Framework.Base.Service.Mapper;
using Framework.User.DataService.Mapper;
using Framework.User.Service.Mapper;

namespace Scene.Manage.UI.MudBlazorServer
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
                mc.AddProfile(new BaseServiceAutoMapperProfile());
                mc.AddProfile(new FrameworkUserDataMapperProfile());
                mc.AddProfile(new FrameworkUserMapperProfile());
                mc.AddProfile(new CommonDataMapperProfile());
                mc.AddProfile(new CommonMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
