using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Managers
{
    public interface IUpdateSelector<TKey, TEntity>
    {
        Task<TEntity> GetEntityForUpdate(TKey id);

        Task<TEntity> GetEntityForUpdate(TKey id, Func<IQueryable<TEntity>, IQueryable<TEntity>> include);
    }
}
