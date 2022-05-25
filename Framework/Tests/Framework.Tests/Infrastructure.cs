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
        public static Action<DbContextOptionsBuilder> GetOptionsAction() => options => options.UseInMemoryDatabase(Guid.NewGuid().ToString());
    }
}
