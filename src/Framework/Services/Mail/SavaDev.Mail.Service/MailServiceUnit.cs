using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Base.Unit.Options;
using SavaDev.Mail.Service.Contract;
using SavaDev.Mail.Service.Contract.Models;
using SavaDev.Mail.Service.Services;
using SavaDev.General.Front.Contract;
using SavaDev.General.Front.Services;
using System.Net.Mail;

namespace SavaDev.Mail.Service
{
    public static class MailServiceUnit
    {
        public static void AddMailRmqService(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<EmailConfiguration>(config.GetSection("Email"));
            services.AddSingleton<SmtpClient>();
            services.AddSingleton<IEmailClient, EmailClient>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IMailSender, MailRmqSender>();
        }

        public static void AddMailDirectService(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<EmailConfiguration>(config.GetSection("Email"));
            services.AddSingleton<SmtpClient>();
            services.AddSingleton<IEmailClient, EmailClient>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IMailSender, MailDirectSender>();
        }
    }
}