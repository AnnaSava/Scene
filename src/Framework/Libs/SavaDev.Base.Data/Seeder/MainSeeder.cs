using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Seeder
{
    public abstract class MainSeeder
    {
        private string appName;

        public MainSeeder(string appName)
        {
            this.appName = appName;
        }

        public async Task Run()
        {
            Console.WriteLine(appName + " started!");

            IConfigurationRoot config = ConfigFile.GetConfiguration(ConfigFile.GetEnvironment());

            var services = new ServiceCollection();
            services.AddLogging(); // TODO: без него не работает идентити. разобраться

            ConfigureServices(config, services);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                using (IServiceScope scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    await Seed(scope);
                }
            }

            Console.WriteLine(appName + " finished!");
        }

        protected abstract void ConfigureServices(IConfigurationRoot config, ServiceCollection services);

        protected abstract List<ISeeder> SetSeeders(IServiceScope scope);

        protected virtual Task MigrateData(IServiceScope scope)
        {
            return Task.CompletedTask;
        }

        private async Task Seed(IServiceScope scope)
        {
            var seeders = SetSeeders(scope);

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

            Console.WriteLine("Start migrating data");
            await MigrateData(scope);
            Console.WriteLine("End migrating data");
        }
    }
}
