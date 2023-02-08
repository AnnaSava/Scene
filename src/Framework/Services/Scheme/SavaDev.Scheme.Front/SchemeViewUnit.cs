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
using SavaDev.Scheme.Front.Contract;
using SavaDev.Scheme.Front.Services;

namespace SavaDev.Scheme.Front
{
    public static class SchemeViewUnit
    {
        // TODO убрать реф на сава...фронт, сделать класс для опшенов
        public static void AddScheme(this IServiceCollection services, IConfiguration config, UnitOptions options)
        {
            services.AddScoped<IColumnViewService, ColumnViewService>();
            services.AddScoped<ITableViewService, TableViewService>();
        }
    }
}
