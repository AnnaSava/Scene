using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Context
{
    public class BaseDbContext : DbContext, IDbContext
    {
        private DbContextOptions _options;
        protected BaseDbOptionsExtension _dbOptionsExtension;

        public BaseDbContext() : base() { }

        public BaseDbContext(DbContextOptions options) : base(options)
        {
            _options = options;

            _dbOptionsExtension = options.Extensions.OfType<BaseDbOptionsExtension>().FirstOrDefault();

            if (_dbOptionsExtension == null)
                throw new Exception($"{nameof(BaseDbOptionsExtension)} not set in {nameof(BaseDbContext)}");
        }

    }
}
