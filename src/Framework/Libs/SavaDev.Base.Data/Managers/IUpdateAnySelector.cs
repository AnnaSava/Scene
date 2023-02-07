using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Managers
{
    public interface IUpdateAnySelector<TEntity>
    {
        Task<TEntity> GetEntityForUpdate(Expression<Func<TEntity, bool>> expression);
    }
}
