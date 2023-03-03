using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Base.Unit.Options;
using SavaDev.Base.Unit;
using SavaDev.General.Data.Contract;
using SavaDev.General.Data.Contract.Context;
using SavaDev.General.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.General.Data
{
    public static class GeneralUnit
    {
        // TODO убрать реф на сава...фронт, сделать класс для опшенов
        public static void AddGeneralData(this IServiceCollection services, IConfiguration config, UnitOptions options)
        {
            services.AddUnitDbContext<IGeneralContext, GeneralContext>(config, options);

            services.AddScoped<IMailTemplateService, MailTemplateService>();

            services.AddScoped<IReservedNameService, ReservedNameService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ILegalDocumentService, LegalDocumentService>();
        }
    }
}
