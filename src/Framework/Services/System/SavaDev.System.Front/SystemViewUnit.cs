using Framework.Mailer;
using Framework.Mailer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Base.Unit.Options;
using SavaDev.System.Data;
using SavaDev.System.Front.Contract;
using SavaDev.System.Front.Services;
using System.Net.Mail;

namespace SavaDev.System.Front
{
    public static class SystemViewUnit
    {
        public static void AddSystem(this IServiceCollection services, IConfiguration config, UnitOptions options)
        {
            services.AddSystemData(config, options);

            services.AddScoped<IPermissionViewService, PermissionViewService>();
            services.AddScoped<IReservedNameViewService, ReservedNameViewService>();
            services.AddScoped<ILegalDocumentViewService, LegalDocumentViewService>();
            services.AddScoped<IMailTemplateViewService, MailTemplateViewService>();


            services.AddSingleton<SmtpClient>();
            services.AddSingleton<IEmailClient, EmailClient>();
                        
            //services.AddScoped<IMailService, MailService>();
        }
    }
}