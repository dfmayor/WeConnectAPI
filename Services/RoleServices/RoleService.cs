using Microsoft.AspNetCore.Identity;
using WeConnectAPI.Models.RoleModels;

namespace WeConnectApi.Services.RoleServices
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<bool> AddRole(string roleName)
        {
            var role = new IdentityRole(roleName);
            var result = await _roleManager.CreateAsync(role);
            return result.Succeeded;
        }

        public async Task<bool> DeleteRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<bool> EditRole(string roleName, string editedRole)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                role.Name = editedRole; // Update the role's name
                var result = await _roleManager.UpdateAsync(role);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<List<string>> GetRoleList()
        {
            List<string> roles = new List<string>();
            foreach (var role in _roleManager.Roles)
            {
                roles.Add(role.Name);
            }
            return roles;
        }
    }
}