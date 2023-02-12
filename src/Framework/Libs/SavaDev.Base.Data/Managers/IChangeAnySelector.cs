using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Managers
{
    public interface IChangeAnySelector<TEntity>
    {
        Task<TEntity> GetEntityForChange(Expression<Func<TEntity, bool>> expression);
    }
}
