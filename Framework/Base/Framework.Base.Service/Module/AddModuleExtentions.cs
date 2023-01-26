using Framework.Base.DataService;
using Framework.Base.DataService.Contract;
using Framework.Base.Types.Enums;
using Framework.Helpers.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Service.Module
{
    public static class AddModuleExtentions
    {
        public static void AddModuleDbContext<TContext>(this IServiceCollection services, IConfiguration config, ModuleSettings moduleSettings)
            where TContext : DbContext
        {
            string tablePrefix = moduleSettings.TablePrefix;
            var namingConvention = config.GetSqlNamingConvention();

            if (namingConvention == NamingConvention.SnakeCase)
            {
                // TODO сделать покрасивше
                tablePrefix = tablePrefix.ToLower() + "_";
            }

            // TODO устарело. Заменить в контекстах, откуда брать конвеншен и префикс, и удалить
            services.AddSingleton(s => new DbContextSettings<TContext>(tablePrefix, namingConvention));

            services.AddDbContext<TContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString(moduleSettings.ConnectionStringName), b => b.MigrationsAssembly(config.GetMigrationAssemblyString()));
                if (namingConvention == NamingConvention.SnakeCase)
                {
                    options.UseSnakeCaseNamingConvention();
                }

                var extension = options.Options.FindExtension<FrameworkDbOptionsExtension>() 
                    ?? new FrameworkDbOptionsExtension { TablePrefix = tablePrefix, NamingConvention = namingConvention };

                ((IDbContextOptionsBuilderInfrastructure)options).AddOrUpdateExtension(extension);
            });
        }
    }
}
