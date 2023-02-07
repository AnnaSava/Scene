using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Base.Unit.Options;
using SavaDev.Base.Unit;
using SavaDev.Scheme.Data.Contract;
using SavaDev.Scheme.Data.Services;
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
        public static void AddSchemeData(this IServiceCollection services, IConfiguration config, UnitOptions options)
        {
            services.AddUnitDbContext<ISchemeContext, SchemeContext>(config, options);

            services.AddScoped<ITableService, TableService>();
            services.AddScoped<ITableConfigService, TableConfigService>();
            services.AddScoped<IColumnService, ColumnService>();
        }
    }
}
