using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Base.Unit.Options;
using SavaDev.General.Data;
using SavaDev.General.Front.Contract;
using SavaDev.General.Front.Services;
using System.Net.Mail;

namespace SavaDev.General.Front
{
    public static class GeneralViewUnit
    {
        public static void AddGeneral(this IServiceCollection services, IConfiguration config, UnitOptions options)
        {
            services.AddGeneralData(config, options);

            services.AddScoped<IPermissionViewService, PermissionViewService>();
            services.AddScoped<IReservedNameViewService, ReservedNameViewService>();
            services.AddScoped<ILegalDocumentViewService, LegalDocumentViewService>();
            services.AddScoped<IMailTemplateViewService, MailTemplateViewService>();


            services.AddSingleton<SmtpClient>();
            //services.AddSingleton<IEmailClient, EmailClient>();
                        
            //services.AddScoped<IMailService, MailService>();
        }
    }
}