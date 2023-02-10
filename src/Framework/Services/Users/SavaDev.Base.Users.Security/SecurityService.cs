using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.User.Data.Entities;
using SavaDev.Base.User.Data.Manager;

namespace SavaDev.Base.Users.Security
{
    public class SecurityService<TKey, TUserEntity, TRoleEntity> : ISecurityService
        where TUserEntity : BaseUser
        where TRoleEntity : BaseRole, new()
    {
        private readonly UserEntityManager<TKey, TUserEntity> _userManager;
        private readonly RoleEntityManager<TKey, TRoleEntity> _roleManager;

        private Dictionary<string, UserInfoCacheModel> memCache = new Dictionary<string, UserInfoCacheModel>();

        public SecurityService(IHttpContextAccessor httpContextAccessor, 
            IDbContext _dbContext, 
            UserManager<TUserEntity> userManager, 
            RoleManager<TRoleEntity> roleManager, 
            IMapper mapper, 
            ILogger logger)
        {
            _userManager = new UserEntityManager<TKey, TUserEntity>(_dbContext, userManager, mapper, logger);
            _roleManager = new RoleEntityManager<TKey, TRoleEntity>(_dbContext, roleManager, logger);
            _httpContextAccessor = httpContextAccessor;
        }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public string GetId()
        {
            return HttpContextUser.GetId(_httpContextAccessor);
        }

        public async Task<bool> IsLocked(string userId)
        {
            if (memCache.ContainsKey(userId) && memCache[userId].IsLocked.HasValue) return memCache[userId].IsLocked.Value;
            // TODO
            var isLocked = false;// await _userManager.IsLocked(userId);
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
                var roleNames = await _userManager.GetRoleNames(long.Parse(userId));

                var existingPermissions = new List<string>();

                foreach (var roleName in roleNames)
                {
                    var role = await _roleManager.GetOneByName(roleName);
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
