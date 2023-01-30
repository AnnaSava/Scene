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

namespace Framework.User.DataService.Services.Filters
{
    public static class AppUserFilterExtentions
    {
        public static IQueryable<AppUser> ApplyFilters(this IQueryable<AppUser> list, AppUserFilterModel filter)
        {
            if (filter == null) return list;

            return list
                .ApplyIsDeletedFilter(filter)
                .ApplyIdFilter(filter)
                .ApplyLoginFilter(filter)
                .ApplyEmailFilter(filter)
                .ApplyEmailConfirmedFilter(filter)
                .ApplyPhoneNumberFilter(filter)
                .ApplyPhoneNumberConfirmedFilter(filter);
        }

        private static IQueryable<AppUser> ApplyLoginFilter(this IQueryable<AppUser> list, AppUserFilterModel filter)
        {
            if (filter.Logins?.Value?.Any() ?? false)
            {
                var rules = GetLoginRules(filter.Logins.Value);
                list = list.Where(rules[filter.Logins.MatchMode]);
            }
            return list;
        }

        private static IQueryable<AppUser> ApplyEmailFilter(this IQueryable<AppUser> list, AppUserFilterModel filter)
        {
            if (filter.Emails?.Value?.Any() ?? false)
            {
                var rules = GetEmailRules(filter.Emails.Value);
                list = list.Where(rules[filter.Emails.MatchMode]);
            }
            return list;
        }

        private static IQueryable<AppUser> ApplyEmailConfirmedFilter(this IQueryable<AppUser> list, AppUserFilterModel filter)
        {
            if (filter.EmailConfirmed != null)
            {
                list = list.Where(m => m.EmailConfirmed == filter.EmailConfirmed);
            }
            return list;
        }

        private static IQueryable<AppUser> ApplyPhoneNumberFilter(this IQueryable<AppUser> list, AppUserFilterModel filter)
        {
            if (filter.PhoneNumbers?.Value?.Any() ?? false)
            {
                var rules = GetPhoneNumberRules(filter.PhoneNumbers.Value);
                list = list.Where(rules[filter.PhoneNumbers.MatchMode]);
            }
            return list;
        }

        private static IQueryable<AppUser> ApplyPhoneNumberConfirmedFilter(this IQueryable<AppUser> list, AppUserFilterModel filter)
        {
            if (filter.PhoneNumberConfirmed != null)
            {
                list = list.Where(m => m.PhoneNumberConfirmed == filter.PhoneNumberConfirmed);
            }
            return list;
        }

        private static IQueryable<AppUser> ApplyLockoutFilter(this IQueryable<AppUser> list, AppUserFilterModel filter)
        {
            if (filter.IsBanned != null)
            {
                list = list.Where(m => m.LockoutEnabled == filter.IsBanned);
            }
            return list;
        }

        private static Dictionary<MatchModeWord, Expression<Func<AppUser, bool>>> GetLoginRules(List<string> value)
        {
            return new Dictionary<MatchModeWord, Expression<Func<AppUser, bool>>>()
                    {
                        { MatchModeWord.Equals, m => m.UserName == value.First() },
                        { MatchModeWord.NotEquals, m => m.UserName != value.First() },
                        { MatchModeWord.StartsWith, m => m.UserName.StartsWith(value.First()) },
                        { MatchModeWord.EndsWith, m => m.UserName.EndsWith(value.First()) },
                        { MatchModeWord.Contains, m => m.UserName.Contains(value.First()) },
                        { MatchModeWord.NotContains, m => !m.UserName.Contains(value.First()) },
                        { MatchModeWord.In, m => value.Contains(m.UserName) },
                        { MatchModeWord.NotIn, m => !value.Contains(m.UserName) },
                    };
        }

        private static Dictionary<MatchModeWord, Expression<Func<AppUser, bool>>> GetEmailRules(List<string> value)
        {
            return new Dictionary<MatchModeWord, Expression<Func<AppUser, bool>>>()
                    {
                        { MatchModeWord.Equals, m => m.Email == value.First() },
                        { MatchModeWord.NotEquals, m => m.Email != value.First() },
                        { MatchModeWord.StartsWith, m => m.Email.StartsWith(value.First()) },
                        { MatchModeWord.EndsWith, m => m.Email.EndsWith(value.First()) },
                        { MatchModeWord.Contains, m => m.Email.Contains(value.First()) },
                        { MatchModeWord.NotContains, m => !m.Email.Contains(value.First()) },
                        { MatchModeWord.In, m => value.Contains(m.Email) },
                        { MatchModeWord.NotIn, m => !value.Contains(m.Email) },
                    };
        }

        private static Dictionary<MatchModeWord, Expression<Func<AppUser, bool>>> GetPhoneNumberRules(List<string> value)
        {
            return new Dictionary<MatchModeWord, Expression<Func<AppUser, bool>>>()
                    {
                        { MatchModeWord.Equals, m => m.PhoneNumber == value.First() },
                        { MatchModeWord.NotEquals, m => m.PhoneNumber != value.First() },
                        { MatchModeWord.StartsWith, m => m.PhoneNumber.StartsWith(value.First()) },
                        { MatchModeWord.EndsWith, m => m.PhoneNumber.EndsWith(value.First()) },
                        { MatchModeWord.Contains, m => m.PhoneNumber.Contains(value.First()) },
                        { MatchModeWord.NotContains, m => !m.PhoneNumber.Contains(value.First()) },
                        { MatchModeWord.In, m => value.Contains(m.PhoneNumber) },
                        { MatchModeWord.NotIn, m => !value.Contains(m.PhoneNumber) },
                    };
        }
    }
}
