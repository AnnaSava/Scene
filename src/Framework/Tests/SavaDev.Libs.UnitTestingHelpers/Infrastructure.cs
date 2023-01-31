using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Enums;

namespace SavaDev.Libs.UnitTestingHelpers
{
    public static class Infrastructure
    {
        public static T GetContext<T>(Func<DbContextOptions<T>, T> creator) where T : DbContext
        {
            var options = GetOptionsAction();

            var optionsBuilder = new DbContextOptionsBuilder<T>();
            options.Invoke(optionsBuilder);

            return creator(optionsBuilder.Options);
        }

        private static Action<DbContextOptionsBuilder> GetOptionsAction() => options =>
        {
            options.UseInMemoryDatabase(Guid.NewGuid().ToString())
            .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            var extension = options.Options.FindExtension<BaseDbOptionsExtension>()
                    ?? new BaseDbOptionsExtension { TablePrefix = "_", NamingConvention = NamingConvention.SnakeCase };

            ((IDbContextOptionsBuilderInfrastructure)options).AddOrUpdateExtension(extension);
        };

        public static ILogger<T> GetLogger<T>()
        {
            return Substitute.For<ILogger<T>>();
        }

        public static void FillContextWithEntities<TContext, TEntity>(TContext context, IEnumerable<TEntity> entities)
            where TContext : DbContext, new()
            where TEntity : class
        {
            context.Database.EnsureCreated();
            context.Set<TEntity>().AddRange(entities);
            context.SaveChanges();
        }

        public static IEnumerable<Dictionary<string, string>> ReadCsvFile(string fileName)
        {
            var strings = new List<string>();
            using(var sr = new StreamReader(fileName))
            {
                while (sr.Peek() >= 0)
                {
                    strings.Add(sr.ReadLine());
                }
            }
            return GetCsvDictionaries(strings);
        }

        public static IEnumerable<Dictionary<string,string>> GetCsvDictionaries(IEnumerable<string> strings)
        {
            if (strings == null || !strings.Any()) return null;

            var stringsArr = strings.ToArray();
            var fieldNames = stringsArr[0].Split(';');

            var fields = new List<Dictionary<string, string>>();

            foreach (var str in strings)
            {
                var dict = new Dictionary<string, string>();

                var valsArr = str.Split(";");
                for(var i = 0; i < fieldNames.Length; i++)
                {
                    dict.Add(fieldNames[i], valsArr[i]);
                }

                fields.Add(dict);
            }
            return fields;
        }
    }
}