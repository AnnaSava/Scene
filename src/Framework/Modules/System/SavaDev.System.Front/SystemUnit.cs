﻿using Framework.Mailer;
using Framework.Mailer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Base.Front.Options;
using SavaDev.System.Data;
using SavaDev.System.Front.Contract;
using SavaDev.System.Front.Services;
using System.Net.Mail;

namespace SavaDev.System.Front
{
    public static class SystemUnit
    {
        public static void AddSystemUnit(this IServiceCollection services, IConfiguration config, ServiceOptions options)
        {
            services.AddSystemDataUnit(config, options);

            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IReservedNameService, ReservedNameService>();
            services.AddScoped<ILegalDocumentService, LegalDocumentService>();

            

            services.AddSingleton<SmtpClient>();
            services.AddSingleton<IEmailClient, EmailClient>();

            
            //services.AddScoped<IMailTemplateService, MailTemplateService>();
            //services.AddScoped<IMailService, MailService>();
        }
    }
}