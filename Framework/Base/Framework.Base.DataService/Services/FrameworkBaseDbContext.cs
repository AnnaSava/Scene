using Framework.Base.DataService.Contract;
using Framework.Base.DataService.Contract.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Services
{
    public class FrameworkBaseDbContext : DbContext, IDbContext
    {
        private DbContextOptions _options;
        protected FrameworkDbOptionsExtension _frameworkDbOptionsExtension;

        public FrameworkBaseDbContext() : base() { }

        public FrameworkBaseDbContext(DbContextOptions options) : base (options)
        {
            _options = options;

            _frameworkDbOptionsExtension = options.Extensions.OfType<FrameworkDbOptionsExtension>().FirstOrDefault();

            if (_frameworkDbOptionsExtension == null)
                throw new Exception($"{nameof(FrameworkDbOptionsExtension)} not set in {nameof(FrameworkBaseDbContext)}");
        }

    }
}
