using AutoMapper;
using Framework.Base.Service.Mapper;
using Framework.MailTemplate.Data.Mapper;
using Framework.MailTemplate.Service.Mapper;
using Framework.User.DataService.Mapper;
using Framework.User.Service.Mapper;
using SavaDev.System.Data;
using SavaDev.System.Front.Mapper;

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
                mc.AddProfile(new AppUserDataMapperProfile());
                mc.AddProfile(new AppUserMapperProfile());
                mc.AddProfile(new SystemDataMapperProfile());
                mc.AddProfile(new SystemMapperProfile());
                mc.AddProfile(new MailTemplateDataMapperProfile());
                mc.AddProfile(new MailTemplateMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
