using Framework.Helpers.Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sava.Libs.WebModule;
using SavaDev.Base.Unit.Options;
using SavaDev.System.Front;
using SavaDev.Users.Data;
using SavaDev.Users.Front;
using System;
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

            //services.AddUnitDbContext<AppUserContext>(config, new ModuleSettings("Ap", "IdentityConnection"));
            //services.AddUnitDbContext<MailTemplateContext>(config, new ModuleSettings("Ml", "IdentityConnection"));

            services.AddUsers(config, new UnitOptions(UnitCode.AppUsers, "Default{0}Connection"));
            services.AddSystem(config, new UnitOptions(UnitCode.System, "Default{0}Connection"));

            //services.AddAppUser(config);
            //services.AddMailTemplate(config);

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
            var context = scope.ServiceProvider.GetService<UsersContext>();
            var mgr = scope.ServiceProvider.GetService<UserManager<User>>();
            var roleMgr = scope.ServiceProvider.GetService<RoleManager<Role>>();
            await new UsersContextSeeder(context, mgr, roleMgr).Seed();

            //var mailTemplateContext = scope.ServiceProvider.GetService<MailTemplateContext>();
            //await new MailTemplateContextSeeder(mailTemplateContext).Seed();
        }
    }
}
