using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Base.Unit.Options;
using SavaDev.Base.Unit;
using SavaDev.System.Data.Contract;
using SavaDev.System.Data.Contract.Context;
using SavaDev.System.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.System.Data
{
    public static class SystemUnit
    {
        // TODO убрать реф на сава...фронт, сделать класс для опшенов
        public static void AddSystemData(this IServiceCollection services, IConfiguration config, UnitOptions options)
        {
            services.AddUnitDbContext<ISystemContext, SystemContext>(config, options);

            services.AddScoped<IMailTemplateService, MailTemplateService>();

            services.AddScoped<IReservedNameService, ReservedNameService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ILegalDocumentService, LegalDocumentService>();
        }
    }
}
