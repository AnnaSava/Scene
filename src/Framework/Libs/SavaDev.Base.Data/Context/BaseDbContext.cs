using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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

        private IDbContextTransaction _transaction;

        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public int Commit()
        {
            int rows = 0;
            try
            {
                rows = SaveChanges();
                _transaction.Commit();
            }
            finally
            {
                _transaction.Dispose();                
            }
            return rows;
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }

    }
}
