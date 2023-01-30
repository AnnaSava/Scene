using Framework.User.DataService.Contract;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Services
{
    public class SecurityService<TUserEntity, TRoleEntity> : ISecurityService<TUserEntity, TRoleEntity>
        where TUserEntity : BaseUser
        where TRoleEntity : BaseRole
    {
        private readonly IUserManagerAdapter<TUserEntity> _userManager;
        private readonly IRoleManagerAdapter<TRoleEntity> _roleManager;

        private Dictionary<string, UserInfoCacheModel> memCache = new Dictionary<string, UserInfoCacheModel>();

        public SecurityService(IUserManagerAdapter<TUserEntity> userManager, IRoleManagerAdapter<TRoleEntity> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> IsLocked(string userId)
        {
            if (memCache.ContainsKey(userId) && memCache[userId].IsLocked.HasValue) return memCache[userId].IsLocked.Value;
            var isLocked = await _userManager.IsLocked(userId);
            memCache.Add(userId, new UserInfoCacheModel { IsLocked = isLocked });
            return memCache[userId].IsLocked.Value;
        }

        // TODO пока не знаю, что сделать, чтобы это было не так больно. 
        public async Task<bool> HasRequiredPermissions(string userId, IEnumerable<string> permissions)
        {
            if (await IsLocked(userId)) return false;

            if (!memCache.ContainsKey(userId) || memCache[userId].Permissions == null || !memCache[userId].Permissions.Any())
            {
                // я не понимаю, почему в дефолтном манагере нельзя сразу получить айдишники или роли целиком
                var roleNames = await _userManager.GetRolesAsync(long.Parse(userId));

                var existingPermissions = new List<string>();

                foreach (var roleName in roleNames)
                {
                    var role = await _roleManager.GetRoleByNameAsync(roleName);

                    var claims = (await _roleManager.GetClaims(role)).Where(m => m.Type == "permission").Select(m => m.Value);

                    existingPermissions.AddRange(claims);
                }

                if (memCache.ContainsKey(userId))
                {
                    memCache[userId].Permissions = existingPermissions.Distinct();
                }
                else
                {
                    memCache.Add(userId, new UserInfoCacheModel { Permissions = existingPermissions.Distinct() });
                }
            }

            return permissions.All(m => memCache[userId].Permissions.Contains(m));
        }
    }
}
