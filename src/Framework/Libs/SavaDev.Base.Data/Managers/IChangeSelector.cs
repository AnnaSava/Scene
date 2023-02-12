using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Managers
{
    public interface IChangeSelector<TKey, TEntity>
    {
        Task<TEntity> GetEntityForChange(TKey id);

        Task<TEntity> GetEntityForChange(TKey id, Func<IQueryable<TEntity>, IQueryable<TEntity>> include);
    }
}
