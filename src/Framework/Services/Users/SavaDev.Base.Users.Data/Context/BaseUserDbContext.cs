using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Entities.Interfaces;
using SavaDev.Base.Data.Exceptions;
using SavaDev.Base.User.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Context
{
    public class BaseUserDbContext<TUser, TRole> : IdentityDbContext<TUser, TRole, long, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
        where TUser : BaseUser
        where TRole : BaseRole
    {
        private DbContextOptions _options;
        protected BaseDbOptionsExtension _dbOptionsExtension;

        public BaseUserDbContext() : base() { }

        public BaseUserDbContext(DbContextOptions options) : base(options)
        {
            _options = options;

            _dbOptionsExtension = options.Extensions.OfType<BaseDbOptionsExtension>().FirstOrDefault();

            if (_dbOptionsExtension == null)
                throw new Exception($"{nameof(BaseDbOptionsExtension)} not set in {nameof(BaseDbContext)}");
        }

        public async Task<TEntity> GetEntityForChange<TEntity>(Expression<Func<TEntity, bool>> expression)
            where TEntity : class
        {
            throw new NotImplementedException();
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

    }
}
