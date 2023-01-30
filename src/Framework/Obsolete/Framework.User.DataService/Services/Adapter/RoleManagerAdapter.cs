using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Services
{
    public class RoleManagerAdapter<TRoleEntity> : IRoleManagerAdapter<TRoleEntity> 
        where TRoleEntity : BaseRole
    {
        internal const string PermissionClaimType = "permission";

        private readonly RoleManager<TRoleEntity> _roleManager;

        public RoleManagerAdapter(RoleManager<TRoleEntity> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<TRoleEntity> CreateAsync(TRoleEntity role)
        {
            var result = await _roleManager.CreateAsync(role);
            HandleResult(result);
            return role;
        }

        public async Task<TRoleEntity> UpdateAsync(TRoleEntity role)
        {
            role.LastUpdated = DateTime.Now;
            var result = await _roleManager.UpdateAsync(role);
            HandleResult(result);
            return role;
        }


        public async Task<TRoleEntity> GetRoleByNameAsync(string name)
        {
            return await _roleManager.FindByNameAsync(name);
        }

        public async Task CreatePermissions(TRoleEntity role, IEnumerable<string> permissions)
        {
            if (permissions == null) return;

            permissions = permissions.Distinct();

            foreach (var permission in permissions)
            {
                var claim = new Claim(PermissionClaimType, permission);
                var result = await _roleManager.AddClaimAsync(role, claim);
                HandleResult(result);
            }
        }

        public async Task UpdatePermissions(TRoleEntity role, IEnumerable<string> permissions)
        {
            if (permissions == null) return;

            permissions = permissions.Distinct();

            var curClaims = await _roleManager.GetClaimsAsync(role);
            var curPermissions = curClaims.Where(m => m.Type == PermissionClaimType);

            var deletedPermissions = curPermissions.Where(m => !permissions.Contains(m.Value));
            foreach (var deletedPermission in deletedPermissions)
            {
                var result = await _roleManager.RemoveClaimAsync(role, deletedPermission);
                HandleResult(result);
            }

            foreach (var permission in permissions)
            {
                if (curPermissions.Any(m => m.Type == PermissionClaimType && m.Value == permission))
                    continue;

                var result = await _roleManager.AddClaimAsync(role, new Claim(PermissionClaimType, permission));
                HandleResult(result);
            }
        }

        public async Task<IEnumerable<Claim>> GetClaims(TRoleEntity role)
        {
            return await _roleManager.GetClaimsAsync(role);
        }

        private void HandleResult(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.First().Description);
            }
        }
    }
}
