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
using Framework.Base.Types;

namespace Framework.MailTemplate
{
    public static class MailTemplateModule
    {
        public static void AddMailTemplate(this IServiceCollection services, IConfiguration config)
        {
            services.AddModuleDbContext<MailTemplateContext>(config, new ModuleSettings("Ml", "IdentityConnection"));

            services.AddSingleton<SmtpClient>();
            services.AddSingleton<IEmailClient, EmailClient>();

            services.AddScoped<IMailTemplateDbService, MailTemplateDbService>();
            services.AddScoped<IMailTemplateService, MailTemplateService>();
            services.AddScoped<IMailService, MailService>();
        }
    }
}
