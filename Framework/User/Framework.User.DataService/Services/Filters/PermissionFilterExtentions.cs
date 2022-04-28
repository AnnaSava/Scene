using Framework.Base.Types.Enums;
using Framework.User.DataService.Contract.Models;
using Framework.User.DataService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Services.Filters
{
    public static class PermissionFilterExtentions
    {
        public static IQueryable<Permission> ApplyFilters(this IQueryable<Permission> list, PermissionFilterModel filter)
        {
            if (filter == null) return list;

            return list
                .ApplyNameFilter(filter);
        }

        private static IQueryable<Permission> ApplyNameFilter(this IQueryable<Permission> list, PermissionFilterModel filter)
        {
            if (filter.Name?.Value?.Any() ?? false)
            {
                var rules = GetNameRules(filter.Name.Value);
                list = list.Where(rules[filter.Name.MatchMode]);
            }
            return list;
        }

        private static Dictionary<MatchModeWord, Expression<Func<Permission, bool>>> GetNameRules(List<string> value)
        {
            return new Dictionary<MatchModeWord, Expression<Func<Permission, bool>>>()
                    {
                        { MatchModeWord.Equals, m => m.Name == value.First() },
                        { MatchModeWord.NotEquals, m => m.Name != value.First() },
                        { MatchModeWord.StartsWith, m => m.Name.StartsWith(value.First()) },
                        { MatchModeWord.EndsWith, m => m.Name.EndsWith(value.First()) },
                        { MatchModeWord.Contains, m => m.Name.Contains(value.First()) },
                        { MatchModeWord.NotContains, m => !m.Name.Contains(value.First()) },
                        { MatchModeWord.In, m => value.Contains(m.Name) },
                        { MatchModeWord.NotIn, m => !value.Contains(m.Name) },
                    };
        }
    }
}
