﻿using Framework.Helpers.Config;
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

            services.AddDbContext<FrameworkUserDbContext>(options =>
              options.UseNpgsql(config.GetConnectionString("IdentityConnection")));

            services.AddMailTemplate(config);

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddFrameworkUser(config);

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
            var context = scope.ServiceProvider.GetService<FrameworkUserDbContext>();
            var mgr = scope.ServiceProvider.GetService<UserManager<FrameworkUser>>();
            var roleMgr = scope.ServiceProvider.GetService<RoleManager<FrameworkRole>>();
            await new AppUserContextSeeder(context, mgr, roleMgr).Seed();

            var mailTemplateContext = scope.ServiceProvider.GetService<MailTemplateContext>();
            await new MailTemplateContextSeeder(mailTemplateContext).Seed();
        }
    }
}
