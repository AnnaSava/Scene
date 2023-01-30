using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Tests
{
    internal static class Infrastructure
    {
        public static T GetContext<T>(Func<DbContextOptions<T>, T> creator) where T : DbContext
        {
            var options = GetOptionsAction();

            var optionsBuilder = new DbContextOptionsBuilder<T>();
            options.Invoke(optionsBuilder);

            return creator(optionsBuilder.Options);
        }

        public static Action<DbContextOptionsBuilder> GetOptionsAction() => options => options.UseInMemoryDatabase(Guid.NewGuid().ToString());
    }
}
