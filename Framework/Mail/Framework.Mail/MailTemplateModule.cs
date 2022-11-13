using AutoMapper;
using Framework.Mail;
using Framework.Mail.Services;
using Framework.Mailer;
using Framework.Mailer.Services;
using Framework.MailTemplate.Data;
using Framework.MailTemplate.Data.Contract;
using Framework.MailTemplate.Data.Services;
using Framework.MailTemplate.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;
using Framework.Base.Service.Module;

namespace Framework.MailTemplate
{
    public static class MailTemplateModule
    {
        public static void AddMailTemplate(this IServiceCollection services, IConfiguration config)
        {
            var connection = config.GetConnectionString("IdentityConnection");
            var migrationsAssembly = config.GetSection("MigrationsAssemblies:Default").Value;

            //services.AddDbContext<MailTemplateContext>(options =>
            //    options.UseNpgsql(connection, b => b.MigrationsAssembly(migrationsAssembly)));

            services.AddModuleDbContext<MailTemplateContext>(config, new ModuleSettings("Ml", "IdentityConnection"));

            services.AddSingleton<SmtpClient>();
            services.AddSingleton<IEmailClient, EmailClient>();

            var cultures = config["Cultures"].Split(',');

            services.AddScoped<IMailTemplateDbService>(s => new MailTemplateDbService(
                s.GetService<MailTemplateContext>(),
                cultures,
                s.GetService<IMapper>()));

            services.AddScoped<IMailTemplateService>(s => new MailTemplateService(
                s.GetService<IMailTemplateDbService>(),
                s.GetService<IMapper>()));

            services.AddScoped<IMailService>(s => new MailService(
                s.GetService<IMailTemplateService>(),
                s.GetService<IEmailClient>()));
        }
    }
}
