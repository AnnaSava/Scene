using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Enums;
using SavaDev.Base.User.Data.Entities;
using SavaDev.Users.Data;
using SavaDev.Users.Data.Entities;

namespace SavaDev.Libs.UnitTestingHelpers
{
    public static class TestsInfrastructure
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

        #region Identity

        public static UserManager<User> GetUserManager(UsersContext context)
        {
            var userStore = new UserStore<User, Role, UsersContext, long>(context);

            var options = Substitute.For<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Value.Returns(idOptions);
            var userValidators = new List<IUserValidator<User>>();
            var validator = Substitute.For<IUserValidator<User>>();
            userValidators.Add(validator);
            var pwdValidators = new List<PasswordValidator<User>>();
            pwdValidators.Add(new PasswordValidator<User>());
            var userManager = new UserManager<User>(userStore, options, new PasswordHasher<User>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                Substitute.For<ILogger<UserManager<User>>>());
            validator.ValidateAsync(userManager, Arg.Any<User>())
                .Returns(Task.FromResult(IdentityResult.Success));
            return userManager;
        }

        public static RoleManager<Role> GetRoleManager(UsersContext context)
        {
            var roleStore = new RoleStore<Role, UsersContext, long, UserRole, RoleClaim>(context);
            return new RoleManager<Role>(
                roleStore,
                new List<IRoleValidator<Role>>(),
                new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(),
                Substitute.For<ILogger<RoleManager<Role>>>());
        }

        #endregion

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