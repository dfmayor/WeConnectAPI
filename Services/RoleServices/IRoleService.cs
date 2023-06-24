using Microsoft.AspNetCore.Identity;

namespace WeConnectApi.Services.RoleServices
{
    public interface IRoleService
    { 
        Task<bool> AddRole(string roleName);
        Task<bool> EditRole(string roleName, string editedRole);
        Task<bool> DeleteRole(string roleName);
        Task<List<string>> GetRoleList();
    }
}