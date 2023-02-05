using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Base.Front.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Data
{
    public static class SchemeUnit
    {
        // TODO убрать реф на сава...фронт, сделать класс для опшенов
        public static void AddSystemDataUnit(this IServiceCollection services, IConfiguration config, ServiceOptions options)
        {
            services.AddUnitDbContext<ISchemeContext, UnitContext>(config, options);

            services.AddScoped<IMailTemplateService, MailTemplateService>();

            services.AddScoped<IReservedNameService, ReservedNameService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ILegalDocumentService, LegalDocumentService>();
        }
    }
}
