using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Entities;
using Framework.Base.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Services
{
    [Obsolete]
    public static class FilterExtentions
    {
        [Obsolete]
        public static IQueryable<TEntity> ApplyIdFilter<TEntity, TFilterModel>(this IQueryable<TEntity> list, TFilterModel filter)
            where TEntity : IEntity<long>
            where TFilterModel : ListFilterModel
        {
            if (filter == null) return list;

            if (filter.Ids?.Value?.Any() ?? false)
            {
                var rules = GetIdRules<TEntity>(filter.Ids.Value);
                list = list.Where(rules[filter.Ids.MatchMode]);
            }

            return list;
        }
        [Obsolete]
        public static IQueryable<TEntity> ApplyIntIdFilter<TEntity, TFilterModel>(this IQueryable<TEntity> list, TFilterModel filter)
            where TEntity : IEntity<int>
            where TFilterModel : ListFilterModel<int>
        {
            if (filter == null) return list;

            if (filter.Ids?.Value?.Any() ?? false)
            {
                var rules = GetIntIdRules<TEntity>(filter.Ids.Value);
                list = list.Where(rules[filter.Ids.MatchMode]);
            }

            return list;
        }
        [Obsolete]
        public static IQueryable<TEntity> ApplyIsDeletedFilter<TEntity, TFilterModel>(this IQueryable<TEntity> list, TFilterModel filter)
            where TEntity : IEntityRestorable
            where TFilterModel : IFilterIsDeleted
        {
            list = list.Where(m => m.IsDeleted == filter.IsDeleted);
            return list;
        }
        [Obsolete]
        private static Dictionary<MatchModeNumeric, Expression<Func<TEntity, bool>>> GetIdRules<TEntity>(List<long> value)
            where TEntity : IEntity<long>
        {
            return new Dictionary<MatchModeNumeric, Expression<Func<TEntity, bool>>>()
                    {
                        { MatchModeNumeric.Equals, m => m.Id == value.First() },
                        { MatchModeNumeric.NotEquals, m => m.Id != value.First() },
                        { MatchModeNumeric.Gt, m => m.Id > value.First() },
                        { MatchModeNumeric.Lt, m => m.Id < value.First() },
                        { MatchModeNumeric.Between,m => m.Id >= value.First() && m.Id <= value.Last() },
                        { MatchModeNumeric.In, m => value.Contains(m.Id) },
                        { MatchModeNumeric.NotIn, m => !value.Contains(m.Id) },
                    };
        }
        [Obsolete]
        private static Dictionary<MatchModeNumeric, Expression<Func<TEntity, bool>>> GetIntIdRules<TEntity>(List<int> value)
            where TEntity : IEntity<int>
        {
            return new Dictionary<MatchModeNumeric, Expression<Func<TEntity, bool>>>()
                    {
                        { MatchModeNumeric.Equals, m => m.Id == value.First() },
                        { MatchModeNumeric.NotEquals, m => m.Id != value.First() },
                        { MatchModeNumeric.Gt, m => m.Id > value.First() },
                        { MatchModeNumeric.Lt, m => m.Id < value.First() },
                        { MatchModeNumeric.Between,m => m.Id >= value.First() && m.Id <= value.Last() },
                        { MatchModeNumeric.In, m => value.Contains(m.Id) },
                        { MatchModeNumeric.NotIn, m => !value.Contains(m.Id) },
                    };
        }
    }
}
