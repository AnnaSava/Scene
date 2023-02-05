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
            var parameters = new Parameters(config, serviceOptions);
            services.AddDbContext<TContext>(options => SetOptions(options, parameters));
        }

        public static void AddUnitDbContext<TInterface, TContext>(this IServiceCollection services, IConfiguration config, ServiceOptions serviceOptions)
            where TContext : DbContext, TInterface
        {
            var parameters = new Parameters(config, serviceOptions);
            services.AddDbContext<TInterface, TContext>(options => SetOptions(options, parameters));
        }

        private static DbContextOptionsBuilder SetOptions(DbContextOptionsBuilder options, Parameters p)
        {
            if (p.DbProvider == DbProviderName.DefaultPgName)
            {
                options.UseNpgsql(p.Connection, b => b.MigrationsAssembly(p.Migrations));
            }
            else if (p.DbProvider == DbProviderName.DefaultMsName)
            {
                options.UseSqlServer(p.Connection, b => b.MigrationsAssembly(p.Migrations));
            }

            var tablePrefix = p.TablePrefix;
            if (p.NamingConvention == NamingConvention.SnakeCase)
            {
                options.UseSnakeCaseNamingConvention();
                tablePrefix = tablePrefix.ToLower() + "_";
            }
            else
            {
                tablePrefix = tablePrefix + ".";
            }

            var extension = options.Options.FindExtension<BaseDbOptionsExtension>()
                ?? new BaseDbOptionsExtension { TablePrefix = tablePrefix, NamingConvention = p.NamingConvention };

            ((IDbContextOptionsBuilderInfrastructure)options).AddOrUpdateExtension(extension);
            return options;
        }

        class Parameters
        {
            public string DbProvider { get; set; }

            public string Connection { get; set; }

            public string Migrations { get; set; }

            public string TablePrefix { get; }

            public NamingConvention NamingConvention { get; set; }

            public Parameters(IConfiguration config, ServiceOptions serviceOptions)
            {
                DbProvider = config.GetDbProvider();
                Connection = config.GetConnectionString(string.Format(serviceOptions.ConnectionStringName, DbProvider));
                Migrations = config.GetMigrationAssemblyString(DbProvider);
                TablePrefix = serviceOptions.TablePrefix;
                NamingConvention = config.GetSqlNamingConvention(DbProvider);
            }
        }
    }
}