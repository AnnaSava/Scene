using Framework.Base.DataService.Contract.Models;
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
    public static class ReservedNameFilterExtentions
    {
        public static IQueryable<ReservedName> ApplyFilters(this IQueryable<ReservedName> list, ReservedNameFilterModel filter)
        {
            if (filter == null) return list;

            return list
                .ApplyTextFilter(filter);
        }

        private static IQueryable<ReservedName> ApplyTextFilter(this IQueryable<ReservedName> list, ReservedNameFilterModel filter)
        {
            if (filter.Text?.Value?.Any() ?? false)
            {
                var rules = GetTextRules(filter.Text.Value);
                list = list.Where(rules[filter.Text.MatchMode]);
            }
            return list;
        }

        private static Dictionary<MatchModeWord, Expression<Func<ReservedName, bool>>> GetTextRules(List<string> value)
        {
            return new Dictionary<MatchModeWord, Expression<Func<ReservedName, bool>>>()
                    {
                        { MatchModeWord.Equals, m => m.Text == value.First() },
                        { MatchModeWord.NotEquals, m => m.Text != value.First() },
                        { MatchModeWord.StartsWith, m => m.Text.StartsWith(value.First()) },
                        { MatchModeWord.EndsWith, m => m.Text.EndsWith(value.First()) },
                        { MatchModeWord.Contains, m => m.Text.Contains(value.First()) },
                        { MatchModeWord.NotContains, m => !m.Text.Contains(value.First()) },
                        { MatchModeWord.In, m => value.Contains(m.Text) },
                        { MatchModeWord.NotIn, m => !value.Contains(m.Text) },
                    };
        }
    }
}
