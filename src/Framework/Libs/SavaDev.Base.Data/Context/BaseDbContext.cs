using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SavaDev.Base.Data.Entities.Interfaces;
using SavaDev.Base.Data.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<TEntity> GetEntityForChange<TEntity>(Expression<Func<TEntity, bool>> expression)
            where TEntity : class
        {
            var entity = await Set<TEntity>().FirstOrDefaultAsync(expression);

            if (entity == null)
                throw new EntityNotFoundException();

            return entity;
        }

        public async Task<TEntity> GetEntityForChange<TKey, TEntity>(TKey id, bool restore = false)
            where TEntity : class, IEntity<TKey>
        {
            var entity = await Set<TEntity>().FirstOrDefaultAsync(m => m.Id.Equals(id));

            if (entity == null)
                throw new EntityNotFoundException();

            if (!(entity is IEntityRestorable))
                return entity;

            var restorableEntity = (IEntityRestorable)entity;

            if (restorableEntity.IsDeleted && !restore)
                throw new EntityNotFoundException();

            if (!restorableEntity.IsDeleted && restore)
                throw new InvalidOperationException();

            restorableEntity.LastUpdated = DateTime.UtcNow;

            return (TEntity)restorableEntity;
        }

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
