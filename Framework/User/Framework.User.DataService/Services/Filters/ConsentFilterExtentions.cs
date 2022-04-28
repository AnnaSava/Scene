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
    public static class ConsentFilterExtentions
    {
        public static IQueryable<Consent> ApplyFilters(this IQueryable<Consent> list, ConsentFilterModel filter)
        {
            if (filter == null) return list;

            return list
                .ApplyIsDeletedFilter(filter)
                .ApplyIntIdFilter(filter)
                .ApplyIsApprovedFilter(filter)
                .ApplyTextFilter(filter);
        }

        private static IQueryable<Consent> ApplyIsApprovedFilter(this IQueryable<Consent> list, ConsentFilterModel filter)
        {
            if (filter.IsApproved != null)
            {
                list = list.Where(m => m.IsApproved == filter.IsApproved);
            }
            return list;
        }

        private static IQueryable<Consent> ApplyTextFilter(this IQueryable<Consent> list, ConsentFilterModel filter)
        {
            if (string.IsNullOrEmpty(filter.Text?.Value) == false)
            {
                var rules = GetTextRules(filter.Text.Value);
                list = list.Where(rules[filter.Text.MatchMode]);
            }
            return list;
        }

        private static Dictionary<MatchModeText, Expression<Func<Consent, bool>>> GetTextRules(string value)
        {
            return new Dictionary<MatchModeText, Expression<Func<Consent, bool>>>()
                    {
                        { MatchModeText.StartsWith, m => m.Text.StartsWith(value.First()) },
                        { MatchModeText.EndsWith, m => m.Text.EndsWith(value.First()) },
                        { MatchModeText.Contains, m => m.Text.Contains(value.First()) },
                        { MatchModeText.NotContains, m => !m.Text.Contains(value.First()) },
                    };
        }
    }
}
