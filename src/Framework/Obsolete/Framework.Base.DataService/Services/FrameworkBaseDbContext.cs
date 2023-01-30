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
    [Obsolete]
    public class FrameworkBaseDbContext : DbContext, IDbContext
    {
        private DbContextOptions _options;
        protected FrameworkDbOptionsExtension _dbOptionsExtension;

        public FrameworkBaseDbContext() : base() { }

        public FrameworkBaseDbContext(DbContextOptions options) : base (options)
        {
            _options = options;

            _dbOptionsExtension = options.Extensions.OfType<FrameworkDbOptionsExtension>().FirstOrDefault();

            if (_dbOptionsExtension == null)
                throw new Exception($"{nameof(FrameworkDbOptionsExtension)} not set in {nameof(FrameworkBaseDbContext)}");
        }

    }
}
