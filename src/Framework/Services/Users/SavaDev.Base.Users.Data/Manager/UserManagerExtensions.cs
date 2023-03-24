using Microsoft.AspNetCore.Identity;
using SavaDev.Base.User.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Users.Data.Manager
{
    public static class UserManagerExtensions
    {
        public static async Task<TEntity> GetOneByLoginOrEmail<TEntity>(this UserManager<TEntity> userManager, string loginOrEmail)
            where TEntity : BaseUser
        {
            TEntity user;

            if (loginOrEmail.Contains("@"))
            {
                user = await userManager.FindByEmailAsync(loginOrEmail);
            }
            else
            {
                user = await userManager.FindByNameAsync(loginOrEmail);
            }
            if (user != null && user.IsDeleted) return null;

            return user;
        }
    }
}
