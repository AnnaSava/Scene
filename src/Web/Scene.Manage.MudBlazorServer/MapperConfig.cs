using AutoMapper;
using Framework.Base.Service.Mapper;
using SavaDev.System.Data;
using SavaDev.System.Front.Mapper;
using SavaDev.Users.Data;
using SavaDev.Users.Front;

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
                mc.AddProfile(new UsersMapperProfile());
                mc.AddProfile(new UsersViewMapperProfile());
                mc.AddProfile(new SystemMapperProfile());
                mc.AddProfile(new SystemViewMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
