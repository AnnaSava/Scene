using Framework.Base.DataService.Services;
using Framework.Base.Types.Enums;
using Framework.User.DataService.Contract.Models;
using Framework.User.DataService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Services
{
    public static class FrameworkRoleFilterExtentions
    {
        public static IQueryable<FrameworkRole> ApplyFilters(this IQueryable<FrameworkRole> list, FrameworkRoleFilterModel filter)
        {
            if (filter == null) return list;

            return list
                .ApplyIsDeletedFilter(filter)
                .ApplyIdFilter(filter)
                .ApplyNameFilter(filter);
        }

        private static IQueryable<FrameworkRole> ApplyNameFilter(this IQueryable<FrameworkRole> list, FrameworkRoleFilterModel filter)
        {
            if (filter.Names?.Value?.Any() ?? false)
            {
                var rules = GetNameRules(filter.Names.Value);
                list = list.Where(rules[filter.Names.MatchMode]);
            }
            return list;
        }

        private static Dictionary<MatchModeWord, Expression<Func<FrameworkRole, bool>>> GetNameRules(List<string> value)
        {
            return new Dictionary<MatchModeWord, Expression<Func<FrameworkRole, bool>>>()
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
