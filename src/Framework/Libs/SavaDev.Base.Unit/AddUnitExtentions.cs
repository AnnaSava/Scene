using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Enums;
using SavaDev.Base.Front.Options;

namespace SavaDev.Base.Unit
{
    public static class AddUnitExtentions
    {
        public static void AddUnitDbContext<TContext>(this IServiceCollection services, IConfiguration config, ServiceOptions serviceOptions)
            where TContext : DbContext
        {
            string tablePrefix = serviceOptions.TablePrefix;
            var namingConvention = config.GetSqlNamingConvention();

            if (namingConvention == NamingConvention.SnakeCase)
            {
                // TODO сделать покрасивше
                tablePrefix = tablePrefix.ToLower() + "_";
            }

            services.AddDbContext<TContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString(serviceOptions.ConnectionStringName), b => b.MigrationsAssembly(config.GetMigrationAssemblyString()));
                if (namingConvention == NamingConvention.SnakeCase)
                {
                    options.UseSnakeCaseNamingConvention();
                }

                var extension = options.Options.FindExtension<BaseDbOptionsExtension>()
                    ?? new BaseDbOptionsExtension { TablePrefix = tablePrefix, NamingConvention = namingConvention };

                ((IDbContextOptionsBuilderInfrastructure)options).AddOrUpdateExtension(extension);
            });
        }

        public static void AddUnitDbContext<TInterface, TContext>(this IServiceCollection services, IConfiguration config, ServiceOptions serviceOptions)
            where TContext : DbContext, TInterface
        {
            string tablePrefix = serviceOptions.TablePrefix;
            var namingConvention = config.GetSqlNamingConvention();

            if (namingConvention == NamingConvention.SnakeCase)
            {
                // TODO сделать покрасивше
                tablePrefix = tablePrefix.ToLower() + "_";
            }

            services.AddDbContext<TInterface, TContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString(serviceOptions.ConnectionStringName), b => b.MigrationsAssembly(config.GetMigrationAssemblyString()));
                if (namingConvention == NamingConvention.SnakeCase)
                {
                    options.UseSnakeCaseNamingConvention();
                }

                var extension = options.Options.FindExtension<BaseDbOptionsExtension>()
                    ?? new BaseDbOptionsExtension { TablePrefix = tablePrefix, NamingConvention = namingConvention };

                ((IDbContextOptionsBuilderInfrastructure)options).AddOrUpdateExtension(extension);
            });
        }
    }
}