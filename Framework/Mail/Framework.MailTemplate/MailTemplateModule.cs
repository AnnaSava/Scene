using AutoMapper;
using Framework.MailTemplate.Data;
using Framework.MailTemplate.Data.Contract;
using Framework.MailTemplate.Data.Services;
using Framework.MailTemplate.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MailTemplate
{
    public static class MailTemplateModule
    {
        public static void AddMailTemplate(this IServiceCollection services, string connection, string migrationsAssembly, IConfiguration config)
        {
            services.AddDbContext<MailTemplateContext>(options =>
                options.UseNpgsql(connection, b => b.MigrationsAssembly(migrationsAssembly)));

            var cultures = config["Cultures"].Split(',');

            services.AddScoped<IMailTemplateDbService>(s => new MailTemplateDbService(
                s.GetService<MailTemplateContext>(),
                cultures,
                s.GetService<IMapper>()));

            services.AddScoped<IMailTemplateService>(s => new MailTemplateService(
                s.GetService<IMailTemplateDbService>(),
                s.GetService<IMapper>()));
        }
    }
}
