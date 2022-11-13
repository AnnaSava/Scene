using Framework.Base.Service.Module;
using Framework.Helpers.Config;
using Framework.MailTemplate;
using Framework.MailTemplate.Data;
using Framework.User.DataService.Entities;
using Framework.User.DataService.Services;
using Framework.User.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Scene.DataSeeder
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Scene Data seeder started!");

            IConfigurationRoot config = ConfigFile.GetConfiguration(ConfigFile.GetEnvironment());
            var services = new ServiceCollection();
            services.AddLogging(); // TODO: без него не работает идентити. разобраться

            //services.AddDbContext<AppUserContext>(options =>
            //  options.UseNpgsql(config.GetConnectionString("IdentityConnection")));

            //services.AddMailTemplate(config);

            services.AddModuleDbContext<AppUserContext>(config, new ModuleSettings("Ap", "IdentityConnection"));
            services.AddModuleDbContext<MailTemplateContext>(config, new ModuleSettings("Ml", "IdentityConnection"));

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddAppUser(config);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                using (IServiceScope scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    await Seed(scope);
                }
            }

            Console.WriteLine("Scene Data seeder finished!");
        }

        private static async Task Seed(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetService<AppUserContext>();
            var mgr = scope.ServiceProvider.GetService<UserManager<AppUser>>();
            var roleMgr = scope.ServiceProvider.GetService<RoleManager<AppRole>>();
            await new AppUserContextSeeder(context, mgr, roleMgr).Seed();

            var mailTemplateContext = scope.ServiceProvider.GetService<MailTemplateContext>();
            await new MailTemplateContextSeeder(mailTemplateContext).Seed();
        }
    }
}
