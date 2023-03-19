using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Base.Unit.Options;
using SavaDev.Infrastructure;
using SavaDev.General.Data;
using SavaDev.General.Front;
using SavaDev.Users.Data;
using SavaDev.Users.Data.Entities;
using SavaDev.Users.Front;
using Scene.Libs.WebModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SavaDev.Base.Data.Seeder;

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

            services.AddUsers(config, new UnitOptions(SceneUnitCode.AppUsers, "Default{0}Connection"));
            services.AddGeneral(config, new UnitOptions(SceneUnitCode.General, "Default{0}Connection"));

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

            // TODO
            var seeders = new List<ISeeder>();
            seeders.Add(new UsersContextSeeder(context, mgr, roleMgr));
            seeders.Add(new SystemContextSeeder(scope.ServiceProvider.GetService<GeneralContext>(), roleMgr));
            // seeders.Add(new SchemeContextSeeder(scope.ServiceProvider.GetService<SchemeContext>()));
            //seeders.Add(new ForumsContextSeeder(scope.ServiceProvider.GetService<ForumsContext>()));
            //seeders.Add(new GuestbookContextSeeder(scope.ServiceProvider.GetService<GuestbookContext>()));
            //seeders.Add(new PlannerContextSeeder(scope.ServiceProvider.GetService<PlannerContext>()));
            //seeders.Add(new TalesContextSeeder(scope.ServiceProvider.GetService<TalesContext>()));
            //seeders.Add(new FilesContextSeeder(scope.ServiceProvider.GetService<FilesContext>()));
            //seeders.Add(new MediaContextSeeder(scope.ServiceProvider.GetService<MediaContext>()));
            //seeders.Add(new ContentContextSeeder(scope.ServiceProvider.GetService<ContentContext>()));
            //seeders.Add(new CommunityContextSeeder(scope.ServiceProvider.GetService<CommunityContext>()));

            foreach (var seeder in seeders)
            {
                try
                {
                    Console.WriteLine($"Start seeding of {seeder.GetType().Name}");
                    await seeder.Seed();
                    Console.WriteLine($"{seeder.GetType().Name} is seeded");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine(ex.InnerException);
                }
            }
        }

        private static async Task Seed0(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetService<UsersContext>();
            var mgr = scope.ServiceProvider.GetService<UserManager<User>>();
            var roleMgr = scope.ServiceProvider.GetService<RoleManager<Role>>();
            await new UsersContextSeeder(context, mgr, roleMgr).Seed();

        }
    }
}
